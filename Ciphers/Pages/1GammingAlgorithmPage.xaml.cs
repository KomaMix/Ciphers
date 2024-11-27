using Ciphers.Enums;
using Ciphers.Helpers;
using Ciphers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ciphers.Pages
{
    /// <summary>
    /// Логика взаимодействия для _1GammingAlgorithmPage.xaml
    /// </summary>
    public partial class _1GammingAlgorithmPage : Page
    {
        public _1GammingAlgorithmPage()
        {
            InitializeComponent();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var language = GetLanguage();

                string text = originalText.Text;
                string keyText = key.Text;

                if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(keyText))
                {
                    ShowErrorMessage("Введите текст и ключ.");
                    return;
                }

                bool[] textBits = BinaryConverterService.StringToBinary(text, language);
                bool[] keyBits = BinaryConverterService.StringToBinary(keyText, language);

                originalBinary.Text = string.Join("", textBits.Select(b => b ? "1" : "0"));
                binaryKey.Text = string.Join("", keyBits.Select(b => b ? "1" : "0"));

                bool[] encryptedBits = GammaCipherService.Encrypt(textBits, keyBits);
                cryptogram.Text = string.Join("", encryptedBits.Select(b => b ? "1" : "0"));
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var language = GetLanguage();

                string encryptedBitsStr = cryptogram.Text;
                string keyText = key.Text;

                if (string.IsNullOrEmpty(encryptedBitsStr) || string.IsNullOrEmpty(keyText))
                {
                    ShowErrorMessage("Зашифрованный текст или ключ отсутствуют.");
                    return;
                }

                bool[] encryptedBits = encryptedBitsStr.Select(c => c == '1').ToArray();
                bool[] keyBits = BinaryConverterService.StringToBinary(keyText, language);

                bool[] decryptedBits = GammaCipherService.Decrypt(encryptedBits, keyBits);

                decryptBinary.Text = string.Join("", decryptedBits.Select(b => b ? "1" : "0"));
                decryptText.Text = BinaryConverterService.BinaryToString(decryptedBits, language);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string text = textBox.Text;

                var language = GetLanguage();

                if (!LanguageHelper.CheckTextIsAlphabet(text, language))
                {
                    ShowErrorMessage("Введите текст на правильном языке");
                    textBox.Clear();
                }
            }
        }

        private void ComboBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            originalText?.Clear();
            originalBinary?.Clear();
            key?.Clear();
            binaryKey?.Clear();
            cryptogram?.Clear();
            decryptBinary?.Clear();
            decryptText?.Clear();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox_LostFocus(sender, e);
                Keyboard.ClearFocus();
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show($"Ошибка: {message}", "Ошибка");
        }

        private TextLanguage GetLanguage()
        {
            return LanguageHelper.GetLanguageTypeForComboBox(comboBoxLanguage);
        }
    }
}
