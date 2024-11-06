namespace Domains
{
    public static class ErrorMessages
    {
        // Required
        public const string Required = "هذا الحقل مطلوب";

        // Length and Size
        public const string MaxLength = "لا يمكن أن يتجاوز {0} حرف";
        public const string MinLength = "يجب أن يكون على الأقل {0} حرف";
        public const string ExactLength = "يجب أن يكون {0} حرفًا بالضبط";
        public const string StringLengthRange = "يجب أن يكون بين {0} و {1} حرفًا";
        public const string PasswordLength = "يجب أن تكون كلمة المرور بين 8 و 100 حرف";


        // Range (Numeric and Date)
        public const string Range = "يجب أن يكون بين {0} و {1}";
        public const string GreaterThan = "يجب أن يكون أكبر من {0}";
        public const string LessThan = "يجب أن يكون أقل من {0}";

        // Regular Expressions
        public const string InvalidFormat = "الصيغة غير صالحة";
        public const string InvalidPhoneNumber = "صيغة رقم الهاتف غير صالحة";
        public const string InvalidEmail = "صيغة البريد الإلكتروني غير صالحة. يجب أن تحتوي على @ ورابط نطاق صحيح";

       

        // Password Validations
        public const string PasswordValidation =
            "كلمة المرور يجب أن تكون بين {0} و {1} حرفًا، " +
            "وتحتوي على حرف كبير، حرف صغير، رقم، وحرف خاص واحد على الأقل"; 


        // URLs and Links
        public const string InvalidUrl = "الرابط غير صالح";

        // Date Validation
        public const string InvalidDate = "تاريخ غير صالح";
        public const string FutureDate = "يجب أن يكون التاريخ في المستقبل";
        public const string PastDate = "يجب أن يكون التاريخ في الماضي";

        // Comparison Validations
        public const string MustMatch = "يجب أن يتطابق مع {0}";
        public const string NotEqualTo = "لا يمكن أن يكون مساويًا لـ {0}";

        // Numeric Validations
        public const string PositiveNumber = "يجب أن يكون عددًا موجبًا";
        public const string NegativeNumber = "يجب أن يكون عددًا سالبًا";
        public const string NonZeroNumber = "لا يمكن أن يكون صفرًا";

        // Unique
        public const string NotUnique = "هذا الحقل يجب أن يكون فريدًا";

        // File and Image Validations
        public const string InvalidFileType = "نوع الملف غير صالح";
        public const string FileTooLarge = "حجم الملف يجب ألا يتجاوز {0} ميجابايت";
    }
}
