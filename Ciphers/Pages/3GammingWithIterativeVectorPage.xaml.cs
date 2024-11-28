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
    /// Логика взаимодействия для _3GammingWithIterativeVectorPage.xaml
    /// </summary>
    public partial class _3GammingWithIterativeVectorPage : Page
    {
        public _3GammingWithIterativeVectorPage()
        {
            InitializeComponent();
        }

        private void GenerateGamma_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var language = GetLanguage();

                string text = originalText.Text;

                if (string.IsNullOrEmpty(text))
                {
                    ShowErrorMessage("Введите сначала текст");
                    return;
                }


                bool[] textBits = BinaryConverterService.StringToBinary(AlphabetHelper.GetAlphaForLanguageType(language).FirstOrDefault().ToString(), language);

                bool[] randomKeyBits = GammaGeneratorService.GenerateRandomGamma(textBits.Length);

                binaryKey.Text = string.Join("", randomKeyBits.Select(b => b ? "1" : "0"));


                if (!string.IsNullOrEmpty(iterativeVector.Text))
                {
                    bool[] IV = iterativeVector.Text.Select(c => c == '1').ToArray();

                    var gammaIVBits = GammaCipherWithIterativeVectorService.GetIVGamma(randomKeyBits, IV);

                    //gammeIV.Text = string.Join("", gammaIVBits.Select(b => b ? "1" : "0"));

                    bool[] IV2 = GammaCipherWithIterativeVectorService.GetIV2(randomKeyBits, IV);
                    iterativeVector2.Text = string.Join("", IV2.Select(b => b ? "1" : "0").ToList());
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void GenerateIV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var language = GetLanguage();


                bool[] IV = GammaGeneratorService.GenerateIV(language);

                iterativeVector.Text = string.Join("", IV.Select(b => b ? "1" : "0"));

                if (!string.IsNullOrEmpty(binaryKey.Text))
                {
                    bool[] keyBits = binaryKey.Text.Select(c => c == '1').ToArray();

                    var gammaIVBits = GammaCipherWithIterativeVectorService.GetIVGamma(keyBits, IV);

                    bool[] IV2 = GammaCipherWithIterativeVectorService.GetIV2(keyBits, IV);
                    iterativeVector2.Text = string.Join("", IV2.Select(b => b ? "1" : "0").ToList());

                    //gammeIV.Text = string.Join("", gammaIVBits.Select(b => b ? "1" : "0"));
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var language = GetLanguage();

                string text = originalText.Text;

                if (string.IsNullOrEmpty(text))
                {
                    ShowErrorMessage("Введите текст");
                    return;
                }

                if (string.IsNullOrEmpty(binaryKey.Text))
                {
                    ShowErrorMessage("Сгенерируйте ключ");
                    return;
                }

                bool[] textBits = BinaryConverterService.StringToBinary(text, language);

                originalBinary.Text = string.Join("", textBits.Select(b => b ? "1" : "0"));

                bool[] keyBits = binaryKey.Text.Select(c => c == '1').ToArray();
                bool[] IV = iterativeVector.Text.Select(c => c == '1').ToArray();

                bool[] encryptedBits = GammaCipherWithIterativeVectorService.Encrypt(textBits, keyBits, IV);
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

                if (string.IsNullOrEmpty(encryptedBitsStr))
                {
                    ShowErrorMessage("Зашифрованный текст или ключ отсутствуют.");
                    return;
                }

                if (string.IsNullOrEmpty(binaryKey.Text))
                {
                    ShowErrorMessage("Сгенерируйте ключ");
                    return;
                }

                bool[] encryptedBits = encryptedBitsStr.Select(c => c == '1').ToArray();
                bool[] keyBits = binaryKey.Text.Select(c => c == '1').ToArray();
                bool[] IV = iterativeVector.Text.Select(c => c == '1').ToArray();

                bool[] decryptedBits = GammaCipherWithIterativeVectorService.Decrypt(encryptedBits, keyBits, IV);

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
            binaryKey?.Clear();
            cryptogram?.Clear();
            decryptBinary?.Clear();
            decryptText?.Clear();
            iterativeVector?.Clear();
            //gammeIV?.Clear();
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
