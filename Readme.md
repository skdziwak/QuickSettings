# QuickSettings C# Library
## Description
QuickSettings is a C# Library that makes creating settings GUI fast and simple.<br>
It manages creating GUI and data persistence.

## Example XML file
```xml
<?xml version="1.0" encoding="utf-8">
<SettingsModel title="Settings">
  <Tab title="Main settings">
    <Label size="18" weight="bold">Main settings</Label>
    <Label weight="bold">CheckBoxes</Label>
    <Stack horizontal="true">
      <CheckBox title="Checkbox1" id="cb1"/>
      <CheckBox title="Checkbox2" checked="true" id="cb2"/>
      <CheckBox title="Checkbox3" checked="false" id="cb3"/>
    </Stack>
    <Label weight="bold">RadioButtons</Label>
    <Stack horizontal="true">
      <RadioButton title="RB1" checked="true" id="rb1"/>
      <RadioButton title="RB2" id="rb2"/>
      <RadioButton title="RB3" id="rb3"/>
    </Stack>
    <Label weight="bold">TextBoxes</Label>
    <TextBox title="TextBox1" label_width="100" id="tb1">Text</TextBox>
    <TextBox title="TextBox2" label_width="100" type="integer" id="tb2">3</TextBox>
    <TextBox title="TextBox3" label_width="100" type="decimal" id="tb3">3.14</TextBox>
    <Label weight="bold">Slider</Label>
    <Slider id="slider"/>
  </Tab>
  <Tab title="Info">
    <Label size="18" weight="bold">Info</Label>
    <Label>This is an example SettingsModel</Label>
    
  </Tab>
</SettingsModel>
```

## Example C# Code
```cs
// Creating parser
QuickSettings.Model.Parser parser = new QuickSettings.Model.Parser();

// Parsing model from resources
QuickSettings.SettingsModel model = parser.ParseXmlResource("Example.Resources.settings_model.xml");

// Loading settings
if(File.Exists("settings.xml"))
    model.FromXml(File.ReadAllText("settings.xml"));

// Creating and showing settings window
var window = new QuickSettings.QuickSettingsWindow(model);
window.ShowDialog();

// Saving settings
File.WriteAllText("settings.xml", model.ToXml());

// Retriving data
Dictionary<string, object> settings = model.ToDictionary();
Console.WriteLine(String.Format("Checkbox1: {0}", (bool)settings["cb1"]));
Console.WriteLine(String.Format("Textbox1: {0}", (string)settings["tb1"]));
Console.WriteLine(String.Format("Slider: {0}", (double)settings["slider"]));
```

## Defining custom tags
```cs
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace QuickSettings.Model.Components
{
    // Defining tag name and description
    [ElementClass("Slider", "Decimal number input")]
    class SliderComponent : Setting
    {
        private Slider slider;

        // Used for connecting Setting value with Slider
        public override object Value { get => slider.Value; set => slider.Value = (double)value; }

        // Used for data serialization
        public override string ValueString { 
            get => ((double)Value).ToString(CultureInfo.InvariantCulture); 
            set => Value = double.Parse(value, CultureInfo.InvariantCulture); 
        }

        public override UIElement UIElement => slider;

        // Defining attributes
        // Every property exposed to xml needs a setter that converts value from string
        [ElementProperty("max")]
        public string Max { set => slider.Maximum = double.Parse(value, CultureInfo.InvariantCulture); }

        [ElementProperty("min")]
        public string Min { set => slider.Minimum = double.Parse(value, CultureInfo.InvariantCulture); }

        public SliderComponent()
        {
            slider = new Slider();
            slider.Minimum = 0;
            slider.Maximum = 100;
            slider.Margin = new Thickness(5);
        }

        // Called after parsing xml attributes
        public override void PostInit()
        {
            
        }

    }
}
```

## Built-in XML tags
| Tag | Description | Attributes |
| --- | ----------- | ---------- |
| SettingsModel | Root tag of all settings models. | title<br>width - Default: 800<br>height - Default: 600<br>resizeable - Default: false |
| Tab | Container for all input tags. | title |
| CheckBox | True/False input. | title<br>checked<br>id |
| ComboBox | Item selection tag | id |
| Item | ComboBox Item |  |
| Grid | Grid for other components | rows - Format: "150:0.7*:0.3*"<br>columns - Format: "150:0.7*:0.3*" |
| Group | GroupBox for settings. | title<br>horizontal - Default: false |
| Label | Text label | font<br>size<br>style - (italic, normal or oblique)<br>weight - (Thin, ExtraLight, UltraLight, Light, Normal, Regular, Medium, DemiBold, SemiBold, Bold, ExtraBold, UltraBold, Black, Heavy, ExtraBlack, UltraBlack) |
| RadioButton | Only one of RadioButtons in a Stack can be selected. | title<br>checked - Default: false<br>id |
| Slider | Decimal number input | max<br>min<br>id |
| Stack | Container tag. | horizontal - Default: false |
| TextBox | Text/number input. | title<br>label_width<br>type - (text, integer or decimal)<br>id |
