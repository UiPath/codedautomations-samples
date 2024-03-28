This project is a "how to"/demo for UiPath.Excel coded automations

Structure of this tutorial project + tips for use of the Excel coded:
1. Use excel.UseExcelFile to retrieve a handle to an excel file
2. Use the handle to perform actions. The idea is to be able to perform the same operations using excel coded automations as using the excel modern activities in an xaml file
	The "activities" are implemented in coded as extensions methods for the handle
	To check all the available extension methods type ExcelOperations. and then <Ctrl + Space>
	See UseExcelFile1.Simple.cs
3. By default excel.UseExcelFile uses the excel options from the project settings. This behavior can be changed by overriding the default options.
	Use excel.ExcelProcessScope to override the default options. It emulates the activity "ExcelProcessScope". 
	The IExcelProcess handle returned can be used together with UseExcelFile for better control of the excel application (how excel is opened, if is closed on exit, etc.)
	See UseExcelFile2_MoreOptions.cs and UseExcelFile_3AllOptions.cs	
4. For workbook activities use excel.UseWorkBook to retrieve a handle that can be later used
    The available "activities" are implemented also as extension methods on ExcelOperations class
	Check UseWorkbook_4.cs for more details
5. More samples:
	UseExcelFile_Filter.cs - how to filter excel
	UseExcelFile_PivotTable.cs - how to create a pivot table and to filter it
	UseExcelFile-ExecuteMacro.cs - how to execute a macro
	
