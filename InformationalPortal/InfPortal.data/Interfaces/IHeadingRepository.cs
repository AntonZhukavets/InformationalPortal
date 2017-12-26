using InfPortal.data.Entities;


namespace InfPortal.data.Interfaces
{
    public interface IHeadingRepository
    {
        bool EditHeading(Heading heading);
        bool DeleteHeading(int? id);
        bool AddHeading(Heading heading);
        Heading[] GetHeadings();        

    }
}
