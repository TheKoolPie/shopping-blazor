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
        public AlertType AlertType { get; set; }

        public AlertComponentModel()
        {

        }
        public AlertComponentModel(string title, string message, AlertType alertType)
        {
            Title = title;
            Message = message;
            AlertType = alertType;
        }
        public static AlertComponentModel CreateSuccessAlert(string title)
        {
            return CreateSuccessAlert(title, "");
        }
        public static AlertComponentModel CreateSuccessAlert(string title, string message)
        {
            return new AlertComponentModel
            {
                Title = title,
                Message = message,
                AlertType = AlertType.Success
            };
        }
        public static AlertComponentModel CreateErrorAlert(string title)
        {
            return CreateErrorAlert(title, "");
        }
        public static AlertComponentModel CreateErrorAlert(string title, string message)
        {
            return new AlertComponentModel
            {
                Title = title,
                Message = message,
                AlertType = AlertType.Danger
            };
        }

        public override string ToString()
        {
            return $"[{Title}] {Message}";
        }
    }
}
