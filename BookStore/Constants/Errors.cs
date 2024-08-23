namespace BookStore.Constants;

public static class Errors
{
    public const string MaxLength = "Length can't be more than {1} characters!";
    public const string Duplicated = "{0} with the same name is already exist!";
    public const string DuplicatedTogether = "Book with the same title and author is already exist!";
    public const string InvalidExtension = ".jpg .jpeg .png only allowed!";
    public const string MaxSize = "file size cannot be more than 2MB!";
    public const string FutureDates = "You can't assign future dates!";

}
