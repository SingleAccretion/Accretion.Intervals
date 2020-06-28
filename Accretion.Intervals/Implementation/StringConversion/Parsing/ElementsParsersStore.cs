using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            static bool IsCompatibleWith<TDelegate>(MethodInfo method) where TDelegate : Delegate => 
                new MethodSignature(typeof(TDelegate).GetMethod("Invoke")).IsCompatibelWith(method);

            var type = typeof(T);
            var parserInfo = type.
                GetMethods(BindingFlags.Public | BindingFlags.Static).
                Where(x => IsCompatibleWith<TParser>(x)).
                SingleOrDefault();
            
            if (parserInfo is null)
            {
                var signature = new MethodSignature(parserInfo).ToString();
                Throw.InvalidOperationException($"The parser method on {type.Name} could not be found. It must have the following signature: {signature}");
            }

            return Delegate.CreateDelegate(typeof(TParser), parserInfo);
        }

        private readonly struct MethodSignature
        {
            public MethodSignature(MethodInfo methodInfo) => MethodInfo = methodInfo;

            public MethodInfo MethodInfo { get; }

            public bool IsCompatibelWith(MethodInfo other) =>
                MethodInfo.Name == other.Name &&
                MethodInfo.ReturnType == other.ReturnType &&
                MethodInfo.GetParameters().
                Select(x => new ParameterSignature(x)).
                Zip(other.GetParameters(), (x, y) => x.IsCompatibleWith(y)).
                All(x => x is true);

            public override string ToString()
            {
                var accessibilityModifier = MethodInfo.IsPublic ? "public" : "private";
                var staticModifier = MethodInfo.IsStatic ? "static" : string.Empty;
                var name = MethodInfo.Name;
                var returnType = MethodInfo.ReturnType.Name;
                var parameters = string.Join(", ", MethodInfo.GetParameters().Select(x => new ParameterSignature(x)));

                return $"{accessibilityModifier} {staticModifier} {returnType} {name}({parameters})";
            }
        }

        private readonly struct ParameterSignature
        {
            public ParameterSignature(ParameterInfo parameterInfo) => ParameterInfo = parameterInfo;

            public ParameterInfo ParameterInfo { get; }

            public bool IsCompatibleWith(ParameterInfo other) => 
                ParameterInfo.ParameterType == other.ParameterType &&
                ParameterInfo.IsOut == other.IsOut;

            public override string ToString() => ParameterInfo.IsOut ? "out" : "" + ParameterInfo.Name;
        }
    }
}
