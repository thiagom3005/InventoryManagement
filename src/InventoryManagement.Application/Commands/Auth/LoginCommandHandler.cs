using InventoryManagement.Application.DTOs.Auth;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Commands.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Buscar usuário
        var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (user == null)
            throw new BusinessRuleException("Usuário ou senha inválidos");

        // Verificar senha
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new BusinessRuleException("Usuário ou senha inválidos");

        // Verificar se usuário está ativo
        if (!user.IsActive)
            throw new BusinessRuleException("Usuário desativado");

        // Atualizar último login
        user.UpdateLastLogin();
        await _userRepository.UpdateAsync(user, cancellationToken);
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
