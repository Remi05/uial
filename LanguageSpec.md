# Language Spec

## Base Interactions

Base Interactions are defined using the name of a core interaction or the name of a high-level (named) interaction, followed by parantheses, which can optionally contain parameters.

Base Interactions must be prefixed by two colons (`::`) and, optionally, a conytext in which the interaction should be executed.
If no context is provided, the current context in which the interaction is used.
For example, if a high-level interaction is defined within a context and one of its base interactions doesn't specify a context, the context parent context of the interaction would be used.
There are also some rare core interactions which do not require a context, such as `Wait()`.

General format:
```
<base_context>::<interaction_name>(<params>)
```

Examples:
```
// Core interaction on current context.
::Invoke()

// Core interaction which doesn't require a context.
::Wait()

// Named interaction on current context.
::MyNameInteraction()

// Core interaction on control.
Button[Name="MyButton"]::Invoke()

// Core interaction on named context.
MyNamedContext::SetTextValue("hello")

// Named interaction on named context.
MyNamedContext::MyNamedInteraction()

// Named interaction on composite named context.
NamedContext1::NamedContext2::MyNamedInteraction()

// Core interaction on composite control context.
Pane[Name="MyPane"]::Button[Name="MyButton"]::Invoke()

// Core interaction on mixed composite context.
MyNamedContext::Slider[Name="MySlider"]::SetRangeValue("0.1")
```


## Comments

Comments are defined using two forward slashes (`//`). They can be written at the root scope or in any other scope (Context, Interaction, Scenario, etc.).

General Format:
```
//<comment>
```

Examples:
```
// This is a comment in the root scope.
scenario MyScenario:
    ...

interaction MyInteraction:
    // This is a comment in an interaction.
    ...
```


## Conditions

Conditions are defined by writing the name of a property followed by the equal sign (`=`) and the property value (either a litteral or a refernce). 
If a condition has multiple properties, they must be seperated by a comma (`,`).

General Format:
```
<property>=<value>
<property1>=<value1>, <property2>=<value2>
```

Examples:
```
// Single condition.
Name="MyControlName"

// Composite condition.
Name="MyButtonName", ControlType="Button"
```


## Contexts

Contexts are defined using the identifier `context` followed by the name you want to give the context.

General format: 
```
context <context_name>(<params>) [<root_condition>] {<unique_condition>}:
<body>
```

Examples:
```
context MyContext:

context MyContextWithParams($myParam1, $myParam2):

context MyContextWithRootCondition [ControlType="Pane", Name="MyPane"]:

context MyContextWithUniqueCondition {ControlType="TitleBar", Name="MyTitleBar"}:

context MyContextWithEverythigng($myParam) [Name="MyRootElement"] {Name="MyUniqueElement"}:
```

You can also use context parameters in the context's conditions:
```
context MyContext($myParam) [ControlType="Button", Name=$myParam]:
```


## Imports

Imports are defined using the identifier `import` followed by the relative file path of the script to import between single quotes (`''`), with at least one whitespace character between the identifier and the file path.

If only a file name is given, the file is searched for in the directory where the script being parsed resides.
If the file is in another direcotry, you can use the relative path to that file from the file currently being parsed.

General format:
```
import '<relative_file_path>.uial'
```

Examples:
```
// File in same directory as current file.
import 'myscript.uial'
import './myscript.uial'

// File in sub-directory of current file.
import 'MySubDirectory/yscript.uial'
import './MySubDirectory/myscript.uial'

// File not in the current subtree.
import '../MyOtherParentDirectory/myscript.uial'
```


## Interactions

Interactions are defined using the `interaction` identifier followed by the name you want to given the interaction.

Interactions can also have parameters, these need to be written between parantheses, after the interaction's name.
Parameters are reference values, so they need to be written using a dollar signe followed by the parameter's name (eg. `$myParam`).

The body of an interaction consists of Base Interactions written with an additional indent (just like Scenarios).

General Format:
```
interaction <interaction_name>(<params>):
    <base_interaction1>
    <base_interaction2>
    ...
```

Examples:
```
// Interaction without parameters.
interaction MyInteraction:
    Button[Name="MyButton"]::Invoke()

// Interaction with parameters.
interaction MyInteraction($editName, $editValue):
    Edit[Name=$editName]::SetTextValue($editValue)
```


## Scenarios

Scenarios are defined using the `scenario` identifier followed the name you want to give the scenario.

The body of a scenario consists of Base Interactions written with an additional indent (just like Interactions).
Scenarios can not currently have parameters (though this might change).

General Format:
```
scenario <scenario_name>:
    <base_interaction1>
    <base_interaction2>
    ...
```

Examples:
```
scenario MyScenario:
    SomeContext::SomeInteraction()
```


## Values

### Litterals

Litteral values are defined by putting a value between quotes (`""`). 

Litteral values can represent text, numbers, element control types, and other types used by core interactions.

General Format:
```
"<litteral_value>"
```

Examples:
```
// Litteral used in a condition.
interaction MyInteraction:
   Button[Name="MyButton"]::Invoke()

// Litteral used as an interaction parameter.
scenario MyScenario:
    MySearchBox::SetTextValue("SomeLitteralValue")

// Litteral values can be numbers.
scenario MyScenario:
    Slider[Name="MySlider"]::SetRangeValue("0.5")

// Litteral values can be control types.
context MyContext [ControlType="Button"]:
    ...
```

### References

Reference values are defined using the dollar sign (`$`) followed by the reference's name.

Reference values can refer to any type that a litteral value can hold (text, number, control type, etc.).

General Format:
```
$<reference_name>
```

Examples:
```
// Reference used in a condition.
interaction MyInteraction($myParam):
    Button[Name=$myParam]::Invoke()

// Reference used as an interaction parameter.
interaction MyInteraction($myParam):
    SomeContext::SomeInteraction($myParam)
```


# Appendix

## Core Base Interactions

- **Close()**         : ToolTip, Window
- *Collapse()         : Not yet implemented*
- *Expand()           : Not yet implemented*
- **Focus()**         : Any
- **Invoke()**        : Button, HeaderItem, Hyperlink, Image, ListItem, MenuItem, SplitButton, TabItem, TreeItem
- **Maximize()**      : Window
- **Minimize()**      : Window
- **Move()**          : Header, HeaderItem, MenuBar, Pane, Thumb, Toolbar, Window
- **Resize()**        : Header, HeaderItem, MenuBar, Pane, ToolBar, Window
- **Select()**        : DataItem, Image, ListItem, MenuItem, RadioButton, TabItem, TreeItem
- **SetRangeValue()** : Edit, ScrollBar, Slider, Text
- **SetTextValue()**  : ComboBox, DataItem, Document, Edit, Hyperlink, ListItem, Text
- **Scroll()**        : ComboBox, DataGrid, Document, List, Pane, Scrollbar, Tab, Tree
- **Toggle()**        : CheckBox, DataItem, ListItem, MenuItem, RadioButton, TreeItem
- **Wait()**          : No/Any context

*Note that the interactions are not guaranteed to be available for all instances of the listed controls, but those not listed definitely do not support the interaction.*

## Condition Properties

- **AutomationId** : Any string
- **ClassName**    : Any string
- **ControlType**  : One of the control types in the section below.
- **Name**         : Any string

## Control Types

- Button
- Calendar
- CheckBox
- ComboBox
- Custom
- DataGrid
- DataItem
- Document
- Edit
- Group
- Header
- HeaderItem 
- Hyperlink
- Image
- List
- ListItem
- Menu
- MenuBar
- MenuItem
- Pane
- ProgressBar
- RadioButton
- ScrollBar
- Separator
- Slider
- Spinner
- SplitButton
- StatusBar
- Tab
- TabItem
- Table
- Text
- Thumb
- TitleBar
- ToolBar
- ToolTip
- Tree
- TreeItem
- Window
