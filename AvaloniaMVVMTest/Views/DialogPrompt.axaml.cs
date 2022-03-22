using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;

namespace AvaloniaMVVMTest.Views
{
    public enum PromptType
    {
        GoodQuestion,
        BadQuestion,
        Login,
        Password,
        FullLogin
    }

    public partial class DialogPrompt : Window
    {
        public DialogPrompt()
        {
            InitializeComponent();
        }
        private Label txtQuestion => this.FindControl<Label>("txtQuestion");
        private TextBox txtResponse => this.FindControl<TextBox>("txtResponse");
        private TextBox txtResponse2 => this.FindControl<TextBox>("txtResponse2");
        private TextBox passwordTxt => this.FindControl<TextBox>("passwordTxt");
        private Button btnOk => this.FindControl<Button>("btnOk");
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            txtResponse.Focus();
        }
        private PromptType type = PromptType.GoodQuestion;
        public DialogPrompt(string question, string title, PromptType promptType = PromptType.GoodQuestion, string defaultValue = "", string okBtnTxt = "OK", object? list = null)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            //form settings
            Title = title;
            txtQuestion.Content = question;
            txtResponse.Text = defaultValue;
            btnOk.Content = okBtnTxt;
            type = promptType;
            switch (type)
            {
                case PromptType.GoodQuestion:
                default:
                    txtResponse.Focus();
                    break;
                case PromptType.Login:
                    txtResponse.IsVisible = false;
                    passwordTxt.IsVisible = true;
                    passwordTxt.Focus();
                    break;
                case PromptType.Password:
                    txtResponse.IsVisible = true;
                    txtResponse2.IsVisible = true;
                    break;
                case PromptType.BadQuestion:
                    txtResponse.Focus();
                    //btnOk.Style = (Style)this.FindResource("RemoveButton");
                    break;
                //case PromptType.FullLogin:
                //    comboBox.ItemsSource = list as BindableCollection<UserInterface>;
                //    comboBox.Visibility = Visibility.Visible;
                //    txtResponse.Visibility = Visibility.Collapsed;
                //    passwordTxt.Visibility = Visibility.Visible;
                //    comboBox.Focus();
                //    break;
            }
        }

        public static async Task<string?> Prompt(Window @window, string question, string title, PromptType promptType = PromptType.GoodQuestion, string defaultValue = "", string okBtnTxt = "OK")
        {
            DialogPrompt inst = new DialogPrompt(question, title, promptType, defaultValue, okBtnTxt);
            await inst.ShowDialog(@window);
            if (inst.DialogResult == true && !string.IsNullOrEmpty(inst.ResponseText))
                return inst.ResponseText;
            return null;
        }
        //public static object? Login(Window @window, string question, string title, ObservableCollection<UserInterface> list = null, PromptType promptType = PromptType.FullLogin, string defaultValue = "", string okBtnTxt = "Login")
        //{
        //    DialogPrompt inst = new DialogPrompt(question, title, promptType, defaultValue, okBtnTxt, list);
        //    inst.ShowDialog(@window);
        //    if (inst.DialogResult == true)
        //        return inst.ResponseObject;
        //    return null;
        //}

        public string? ResponseText
        {
            get
            {
                switch (type)
                {
                    case PromptType.GoodQuestion:
                    case PromptType.BadQuestion:
                    default:
                        return txtResponse.Text;
                    case PromptType.Login:
                        return passwordTxt.Text;
                    case PromptType.Password:
                        if (txtResponse.Text == txtResponse2.Text)
                        {
                            return txtResponse.Text;
                        }
                        else
                        {
                            return null;
                        }
                }
            }
        }
        public object? ResponseObject
        {
            get
            {
                switch (type)
                {
                    default:
                        return null;
                    //case PromptType.FullLogin:
                    //    if ((comboBox.SelectedItem as UserInterface)._SecurityLevel == ArtLib.SecurityLevel.Admin)
                    //    {
                    //        if (new PasswordManager().ComparePasswords((comboBox.SelectedItem as UserInterface).HashedPassword, passwordTxt.Password, (comboBox.SelectedItem as UserInterface).PasswordSalt))
                    //        {
                    //            return comboBox.SelectedItem;
                    //        }
                    //        else
                    //        {
                    //            return null;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        return comboBox.SelectedItem;
                    //    }
                }
            }
        }
        private bool? DialogResult { get; set; }
        private void EnterKeyPressed()
        {
            DialogResult = true;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
