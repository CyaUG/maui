using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Youth.Helpers.Converters
{
    public class IsCorrectAnsConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool retVal = false;
            if (value != null)
            {
                try
                {
                    QuizQuestionAnswer quizQuestionAnswer = value as QuizQuestionAnswer;
                    if (quizQuestionAnswer.isCorrect)
                    {
                        retVal = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsViewerConverter: " + ex);
                }
            }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
