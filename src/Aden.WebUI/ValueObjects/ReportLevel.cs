namespace Aden.WebUI.ValueObjects;

public record ReportLevel
{
    private ReportLevel(){}

    public ReportLevel(bool isSea = false, bool isLea = false, bool isSch = false)
    {
        IsSea = isSea;
        IsLea = isLea;
        IsSch = isSch; 
    }
    public bool IsLea { get; private set; }
    public bool IsSea { get; private set; }
    public bool IsSch { get; private set; }    
    
};