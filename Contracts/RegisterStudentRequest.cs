namespace ProjectAcademy.Contracts;

    public record RegisterStudentRequest(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password
);

