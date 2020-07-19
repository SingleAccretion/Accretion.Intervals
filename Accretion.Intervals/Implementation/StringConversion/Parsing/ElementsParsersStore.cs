using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Accretion.Intervals.StringConversion
{
    internal static class ElementParsers
    {
        private const string ParserName = "Parse";
        private const string TryParserName = "TryParse";

        //The caches are weakly typed because they hold parser for different types
        private static Dictionary<Type, Delegate> _elementParsers;
        private static Dictionary<Type, Delegate> _spanElementParsers;
        private static Dictionary<Type, Delegate> _tryElementParsers;
        private static Dictionary<Type, Delegate> _trySpanElementParsers;

        private static Dictionary<Type, Delegate> ElementParsersStore => _elementParsers ??= new Dictionary<Type, Delegate>();
        private static Dictionary<Type, Delegate> SpanElementParsers => _spanElementParsers ??= new Dictionary<Type, Delegate>();
        private static Dictionary<Type, Delegate> TryElementParsers => _tryElementParsers ??= new Dictionary<Type, Delegate>();
        private static Dictionary<Type, Delegate> TrySpanElementParsers => _trySpanElementParsers ??= new Dictionary<Type, Delegate>();

        public static Parse<T> GetElementParser<T>() =>
            GetParser<Parse<T>>(ElementParsersStore, () => DiscoverParser<T, Parse<T>>(ParserName));

        public static ParseSpan<T> GetSpanElementParser<T>() =>
            GetParser<ParseSpan<T>>(SpanElementParsers, () => DiscoverParser<T, ParseSpan<T>>(ParserName));

        public static TryParse<T> GetTryElementParser<T>() =>
            GetParser<TryParse<T>>(TryElementParsers, () => DiscoverParser<T, TryParse<T>>(TryParserName));

        public static TryParseSpan<T> GetTrySpanElementParser<T>() =>
            GetParser<TryParseSpan<T>>(TrySpanElementParsers, () => DiscoverParser<T, TryParseSpan<T>>(TryParserName));

        private static T GetParser<T>(Dictionary<Type, Delegate> store, Func<Delegate> parserDiscoverer) where T : Delegate
        {            
            if (store.TryGetValue(typeof(T), out var parser)) { }
            else
            {
                parser = parserDiscoverer();
                store.Add(typeof(T), parser);                
            }

            return (T)parser;
        }
        
        private static Delegate DiscoverParser<T, TParser>(string parserName) where TParser : Delegate
        {
            var parserInfo = typeof(TParser).GetMethod("Invoke");
            var parserSignature = new MethodSignature(parserInfo, parserName, MethodAttributes.Public | MethodAttributes.Static);

            var type = typeof(T);
            var parserCandidates = type.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(x => x.Name == parserName);
            
            var parsers = parserCandidates.
                Where(x => parserSignature.IsCompatibelWith(x)).
                Select(x => (Method: x, ParameterCount: x.GetParameters().Length)).
                OrderBy(x => x.ParameterCount);

            var firstParameter = parsers.FirstOrDefault();
            if (firstParameter.Equals(default))
            {
                var signature = new MethodSignature(parserInfo, parserName, MethodAttributes.Public | MethodAttributes.Static).ToString();
                Throw.ParserNotFound(type, signature);
            }

            //Multiple parsers can be found because default parameters are not considered part of the signature
            //In general, some signature differences cannot be resolved with any sane algorithm
            //E. g. Parse(string str, int x = 0) vs Parse(string str, float x = 0.0f)
            //We throw in this case
            var resolvedParsers = parsers.TakeWhile(x => x.ParameterCount == firstParameter.ParameterCount).ToList();
            if (resolvedParsers.Count > 1)
            {
                Throw.OverloadResolutionFailed(type);
            }

            return ShimGenerator.WithDefaultParametersPassed<TParser>(resolvedParsers[0].Method);
        }

        private readonly struct MethodSignature
        {
            private readonly string _name;
            private readonly MethodAttributes _methodAttributes;

            public MethodSignature(MethodInfo methodInfo, string name, MethodAttributes methodAttributes)
            {
                MethodInfo = methodInfo;
                _name = name;
                _methodAttributes = methodAttributes;
            }

            public MethodInfo MethodInfo { get; }

            public bool IsCompatibelWith(MethodInfo other)
            {
                if (MethodInfo.ReturnType != other.ReturnType)
                {
                    return false;
                }

                var parameters = MethodInfo.GetParameters();
                var otherParameters = other.GetParameters().Where(x => !x.HasDefaultValue).ToArray();

                if (parameters.Length != otherParameters.Length)
                {
                    return false;
                }

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (!new ParameterSignature(parameters[i]).IsCompatibleWith(otherParameters[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            public override string ToString()
            {                
                static IEnumerable<string> GetMethodAttributes(MethodSignature signature)
                {
                    if (signature._methodAttributes.HasFlag(MethodAttributes.Public))
                    {
                        yield return "public";
                    }
                    if (signature._methodAttributes.HasFlag(MethodAttributes.Static))
                    {
                        yield return "static";
                    }
                }

                var name = _name;
                var returnType = MethodInfo.ReturnType.Name;
                var parameters = string.Join(", ", MethodInfo.GetParameters().Select(x => new ParameterSignature(x)));

                return $"{string.Join(" ", GetMethodAttributes(this))} {returnType} {name}({parameters})";
            }
        }

        private readonly struct ParameterSignature
        {
            public ParameterSignature(ParameterInfo parameterInfo) => ParameterInfo = parameterInfo;

            public ParameterInfo ParameterInfo { get; }

            public bool IsCompatibleWith(ParameterInfo other) => ParameterInfo.ParameterType == other.ParameterType && ParameterInfo.IsOut == other.IsOut;

            public override string ToString()
            {
                var tokens = new List<object>();
                if (ParameterInfo.IsOut)
                {
                    tokens.Add("out");
                }
                else if (ParameterInfo.IsIn)
                {
                    tokens.Add("in");
                }
                else if (ParameterInfo.ParameterType.IsByRef)
                {
                    tokens.Add("ref");
                }
                
                tokens.Add(ParameterInfo.ParameterType);
                tokens.Add(ParameterInfo.Name);

                return string.Join(" ", tokens);
            }
        }
    }
}
