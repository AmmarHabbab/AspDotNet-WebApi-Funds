namespace WebApi1.Services;

public class PaginationMetadata 
{
    public int TotalItemCount{get;set;}
    public int TotaPageCount{get;set;}
    public int PageSize{get;set;}
    public int CurrentPage{get;set;}

    public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
    {
        TotalItemCount = totalItemCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotaPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
    }
}