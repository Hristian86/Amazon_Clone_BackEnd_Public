using AutoMapper;

namespace AkciqApp.AutoMapper
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
