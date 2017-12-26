using InfPortal.business.DTO;

namespace InfPortal.business.Interfaces
{
    public interface IHeadingProvider
    {
        bool AddHeading(HeadingDTO heading);
        bool EditHeading(HeadingDTO heading);
        bool DeleteHeading(int? id);
        HeadingDTO[] GetHeadings();
    }
}
