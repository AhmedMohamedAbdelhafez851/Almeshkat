namespace Almeshkat_Online_Schools.Utilities
{
    public static class ApiMessages
    {
        // Generic success messages
        public const string Success = "تمت العملية بنجاح.";
        public const string Created = "تم إنشاء العنصر بنجاح.";
        public const string Updated = "تم تحديث العنصر بنجاح.";
        public const string Deleted = "تم حذف العنصر بنجاح.";

        // Error messages
        public const string NotFound = "العنصر غير موجود.";
        public const string Unauthorized = "الرجاء التأكد من تسجيل الدخول.";
        public const string Forbidden = "ليس لديك صلاحيات كافية.";
        public const string ValidationError = "حدث خطأ في التحقق من البيانات.";
        public const string ServerError = "حدث خطأ في الخادم.";

        // Placeholder messages that can take parameters
        public static string GetMismatchMessage(string field) => $"{field} غير مطابق.";
        public static string GetAlreadyExistsMessage(string entity) => $"العنصر {entity} موجود بالفعل.";
        public static string GetRequiredFieldMessage(string field) => $"الحقل {field} مطلوب.";
    }

}
