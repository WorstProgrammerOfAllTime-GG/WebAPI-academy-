namespace ProjectAcademy.Contracts;

    public record LoginRequest(
    string Password,
    string? Email = null,
    string? PhoneNumber = null
);

