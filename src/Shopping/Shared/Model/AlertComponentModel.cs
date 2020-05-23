using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Model
{
    public class AlertComponentModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public AlertType Type { get; set; }

        public AlertComponentModel()
        {

        }
        public AlertComponentModel(string title, string message, AlertType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }

        public static AlertComponentModel CreateSuccessAlert(string title, string message)
        {
            return new AlertComponentModel
            {
                Title = title,
                Message = message,
                Type = AlertType.Success
            };
        }
        public static AlertComponentModel CreateErrorAlert(string title, string message)
        {
            return new AlertComponentModel
            {
                Title = title,
                Message = message,
                Type = AlertType.Danger
            };
        }

        public override string ToString()
        {
            return $"[{Title}] {Message}";
        }
    }
}
