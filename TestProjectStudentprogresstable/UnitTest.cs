using Avalonia.Controls;
using Avalonia.VisualTree;

namespace TestProjectStudentprogresstable
{
    public class TestProjectStudentprogresstable
    {

        [Fact]
        public async void addstudentinlistbox()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();
            var expected = "Иванов Иван Иванович";
            await Task.Delay(100);
            var textBox = mainWindow.GetVisualDescendants().OfType<TextBox>().First(t => t.Name == "TextBoxInput");
            var addButton = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "AddButton");
            textBox.Text = "Иванов Иван Иванович";
            addButton.Command.Execute(addButton.CommandParameter);
            await Task.Delay(50);
            var textBlock = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(t => t.Name == "TextBlockO");
            var result = textBlock.Text;
            Assert.True(result.Equals(expected));
        }
        [Fact]
        public async void addtwostudentsandsavefile()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();
            await Task.Delay(100);
            var textBox = mainWindow.GetVisualDescendants().OfType<TextBox>().First(t => t.Name == "TextBoxInput");
            var listBox = mainWindow.GetVisualDescendants().OfType<ListBox>().First();
            var listBoxItems = listBox.GetVisualDescendants().OfType<ListBoxItem>();
            var deleteButton = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "DeleteButton");
            var addButton = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "AddButton");
            var saveButton = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "SaveButton");
            textBox.Text = "Иванов Иван Иванович";
            addButton.Command.Execute(addButton.CommandParameter);
            listBox.SelectedIndex = 0;
            textBox.Text = "Иванов Иван Васильевич";
            saveButton.Command.Execute(saveButton.CommandParameter);
            await Task.Delay(50);
            Assert.True(listBoxItems.Count() == 2);
        }
    }
}