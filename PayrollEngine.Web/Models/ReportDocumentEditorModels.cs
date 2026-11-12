using System.Text.Json;

namespace PayrollEngine.Web.Models;

public sealed class ReportEmployeeDocumentEditorDefinition
{
    public string MainSectionTitle { get; set; } = "Adatok";

    public string MainWorksheetName { get; set; } = "Adatok";

    public ReportDocumentHeaderEditorDefinition Header { get; set; } = new();

    public List<ReportDocumentSectionEditorDefinition> AdditionalSections
    {
        get;
        set;
    } = [];
}

public sealed class ReportDocumentHeaderEditorDefinition
    : ReportDocumentDataBlockEditorDefinition
{
    public bool IsEnabled { get; set; } = true;
}

public sealed class ReportDocumentSectionEditorDefinition
    : ReportDocumentDataBlockEditorDefinition
{
    public string Code { get; set; } = Guid.NewGuid().ToString("N");

    public string Title { get; set; } = "Új adatrész";

    public string WorksheetName { get; set; } = "Adatok";

    public bool IsEnabled { get; set; } = true;

    public bool StartNewPage { get; set; } = true;
}

public class ReportDocumentDataBlockEditorDefinition
{
    public string DataSource { get; set; } = "EMPLOYEE";

    public ReportDocumentDateFilterEditorDefinition DateFilter
    {
        get;
        set;
    } = new();

    public List<ReportDocumentRelationEditorDefinition> Relations
    {
        get;
        set;
    } = [];

    public List<ReportDocumentFieldEditorDefinition> Fields
    {
        get;
        set;
    } = [];

    public ReportDocumentGroupingEditorDefinition Grouping
    {
        get;
        set;
    } = new();

    public List<ReportDocumentParameterEditorDefinition> Parameters
    {
        get;
        set;
    } = [];

    public List<ReportDocumentFilterEditorDefinition> Filters
    {
        get;
        set;
    } = [];
}

public sealed class ReportDocumentDateFilterEditorDefinition
{
    public string Mode { get; set; } = "NONE";

    public string? DateField { get; set; }

    public string? ValidFromField { get; set; }

    public string? ValidToField { get; set; }
}

public sealed class ReportDocumentRelationEditorDefinition
{
    public string RelationCode { get; set; } = "";

    public string JoinType { get; set; } = "LEFT";

    public ReportDocumentDateFilterEditorDefinition DateFilter
    {
        get;
        set;
    } = new();
}

public sealed class ReportDocumentFieldEditorDefinition
{
    public string Code { get; set; } = Guid.NewGuid().ToString("N");

    public string DisplayName { get; set; } = "";

    public string OutputName { get; set; } = "";

    public ReportDocumentValueEditorDefinition Value
    {
        get;
        set;
    } = new();

    public string Aggregation { get; set; } = "NONE";

    public string? Format { get; set; }
}

public sealed class ReportDocumentValueEditorDefinition
{
    public string Kind { get; set; } = "REFERENCE";

    public string? Reference { get; set; }

    public JsonElement? Constant { get; set; }
}

public sealed class ReportDocumentGroupingEditorDefinition
{
    public string RowMode { get; set; } = "DETAIL";

    public List<string> GroupBy { get; set; } = [];

    public List<string> SplitFilesBy { get; set; } = [];

    public List<string> SortBy { get; set; } = [];
}

public sealed class ReportDocumentParameterEditorDefinition
{
    public string Code { get; set; } = "";

    public string Label { get; set; } = "";

    public string Type { get; set; } = "text";

    public bool Required { get; set; }

    public JsonElement? DefaultValue { get; set; }

    public string? Mode { get; set; }
}

public sealed class ReportDocumentFilterEditorDefinition
{
    public string Field { get; set; } = "";

    public string Operator { get; set; } = "EQUALS";

    public string? Parameter { get; set; }

    public JsonElement? Constant { get; set; }
}