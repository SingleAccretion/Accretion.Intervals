namespace Accretion.Intervals.StringConversion
{
    internal enum TokenType
    {
        Separator = 1,
        Union,
        StartOpen,
        EndOpen,
        StartClosed,
        EndClosed,
        StartSingleton,
        EndSingleton,
        Text,
        End
    }
}
