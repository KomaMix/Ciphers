using Ciphers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ciphers.Helpers
{
    public class LanguageHelper
    {
        public static TextLanguage GetLanguageType(string content)
        {
            if (Enum.TryParse(content, out TextLanguage selectedLanguage))
            {
                return selectedLanguage;
            }

            throw new InvalidOperationException("Невозможно преобразовать выбранный элемент в EncryptLanguage");
        }

        public static TextLanguage GetLanguageTypeForComboBox(Object item)
        {
            var comboBox = item as ComboBox;

            var comboBoxItem = comboBox.SelectedItem as ComboBoxItem;


            if (comboBoxItem != null)
            {
                string selectedContent = comboBoxItem.Content.ToString();

                return GetLanguageType(selectedContent);
            }

            throw new InvalidOperationException("Невозможно преобразовать выбранный элемент в EncryptLanguage");
        }

        public static bool CheckTextIsAlphabet(string text, TextLanguage language)
        {
            string alpha = AlphabetHelper.GetAlphaForLanguageType(language);

            foreach (char ch in text.ToLower())
            {
                if (!alpha.Contains(ch))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
