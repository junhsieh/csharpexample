To install WPF Toolkit, run the following command in the Package Manager Console:

PM&gt; Install-Package WPFToolkit

or

PM&gt; Install-Package DotNetProjects.Wpf.Toolkit

Note: if you are using "DotNetProjects.Wpf.Toolkit" instead of "WPFToolkit", the xml namespace should be:

        xmlns:toolkit1="clr-namespace:System.Windows.Controls;assembly=WPFToolkit"
        xmlns:toolkit2="clr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.Toolkit"