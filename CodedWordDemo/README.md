# Word Coded Automations samples

## Usage

This is a tutorial project containing most of the methods from the Word Coded service:

1. Use word.UseWordDocument to retrieve a handle to a Word file
2. Use the handle to perform actions. The idea is to be able to perform the same operations using Word Coded Automations as using the Word modern activities in an xaml file
	The "activities" are implemented in Coded as extensions methods for the handle
	To check all the available extension methods type WordOperations. and then <Ctrl + Space>
3. By default word.UseWordDocument uses some default options for the resulting handle, but you can change them by using a WordUseOptions object as parameter and set its properties as needed. 
4. Samples:
	UseWordDocument_Basic.cs - basic usage of the handle with default options
	UseWordDocument_WithOptions.cs - usage of the handle with custom options
	UseWordDocument_AddPictureOptions.cs - how to add pictures in a Word document
	UseWordDocument_InsertDataTable.cs - how to insert a DataTable in a Word document
	UseExcelFile-ExecuteMacro.cs - how to execute a macro