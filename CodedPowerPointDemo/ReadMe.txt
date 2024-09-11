﻿This project is a "how to"/demo for UiPath.PowerPoint coded automations

Structure of this tutorial project + tips for use of the PowerPoint coded:
1. Use powerpoint.UsePowerPointPresentation to retrieve a handle to an PowerPoint file
2. Use the handle to perform actions. The idea is to be able to perform the same operations using powerpoint coded automations as using the powerpoint modern activities in an xaml file
	The "activities" are implemented in coded as extensions methods for the handle
	To check all the available methods type <handle_name>. and then <Ctrl + Space>
3 More samples:
 AddNewSlide.cs - Adds a new slide to the presentation
 AddText2Slide.cs - Adds text to a slide into a specific placeholder
 FormatSlideContent.cs - Formats the content of a slide ( due to https://uipath.atlassian.net/browse/STUD-71556 reflection mechanism must be used to invoke FontSizeModificationModel as IFormatSlideModicationModel  
 DeleteSlide.cs
 AddFile2Slide.cs
 AddImageOrVideo2Slide
 

4. Helpers ( this are non needed per se but help a lot ) 

	ConstClass2 - contains string representations of diferent slide format options ( https://support.microsoft.com/en-us/office/what-is-a-slide-layout-99da5716-92ee-4b6a-a0b5-beea45150f3a )

Note: Due to know limitation right now when exectuting a coded powepoint workflow the powerpoint files must be closed