using System;
using System.Windows;

namespace PromptDialog
{
    /// <summary>
    /// Interaction logic for PromptDialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {
        public enum InputType
        {
            Fecha,
            Text,
            Password,
            Numero
        }

        private InputType _inputType = InputType.Text;

        private string _defaultValue;

        public Dialog(string question, string title, string defaultValue = "", InputType inputType = InputType.Text)
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(PromptDialog_Loaded);
            txtQuestion.Text = question;
            Title = title;
            _defaultValue = defaultValue;
            _inputType = inputType;
        }

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            switch (_inputType)
            {
                case InputType.Password:
                    txtPasswordResponse.Visibility = Visibility.Visible;
                    txtPasswordResponse.Focus();
                    break;
                case InputType.Fecha:
                    dtpDateResponse.Visibility = Visibility.Visible;
                    if (!string.IsNullOrEmpty(_defaultValue))
                        dtpDateResponse.SelectedDate = DateTime.Parse(_defaultValue);

                    dtpDateResponse.Focus();
                    break;
                default:
                    txtResponse.Visibility = Visibility.Visible;
                    txtResponse.Text = _defaultValue;
                    txtResponse.Focus();
                    txtResponse.SelectAll();
                    break;
            }
        }

        public static object Prompt(string question, string title, string defaultValue = "", InputType inputType = InputType.Text)
        {
            Dialog inst = new Dialog(question, title, defaultValue, inputType);
            inst.ShowDialog();
            if (inst.DialogResult == true)
                return inst.ResponseText;
            return null;
        }

        public object ResponseText
        {
            get
            {
                switch (_inputType)
                {
                    case InputType.Password: return txtPasswordResponse.Password;
                    case InputType.Fecha: return dtpDateResponse.SelectedDate;
                    default: return txtResponse.Text;
                }
            }
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
