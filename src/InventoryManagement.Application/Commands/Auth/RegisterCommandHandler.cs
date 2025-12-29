using InventoryManagement.Application.DTOs.Auth;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Commands.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Validar se username já existe
        if (await _userRepository.ExistsByUsernameAsync(request.Username, cancellationToken))
            throw new BusinessRuleException($"Username '{request.Username}' já está em uso");

        // Validar se email já existe
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            throw new BusinessRuleException($"Email '{request.Email}' já está em uso");

        // Hash da senha (simplificado - em produção use BCrypt ou similar)
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Criar usuário
        var user = User.Create(
            request.Username,
            request.Email,
            passwordHash,
            request.FullName,
            request.Role
        );

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        // Gerar token JWT
        var token = _jwtService.GenerateToken(user);
        var expiresAt = _jwtService.GetTokenExpiration();

        return new AuthResponse(
            token,
            user.Username,
            user.Email,
            user.FullName,
            user.Role,
            expiresAt
        );
    }
}
