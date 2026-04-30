using Auctera.Admin.Application.Commands;
using Auctera.Identity.Application.Interfaces;
using Auctera.Items.Application.Interfaces;
using Auctera.Persistance.Configurations.Users;

using MediatR;

namespace Auctera.Admin.Application.Handlers.Commands;
public sealed class AcceptLotCommandHandler : IRequestHandler<AcceptLotCommand>
{
    private readonly ILotRepository _lotRepository;
    private readonly IUserRepository _userRepository;

    public AcceptLotCommandHandler(ILotRepository lotRepository, IUserRepository userRepository)
    {
        _lotRepository = lotRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(AcceptLotCommand command, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.GetLotById(command.lotId, cancellationToken);

        if (lot == null)
        {
            throw new ArgumentException("Lot is not found");
        }

        var user = _userRepository.GetUserByIdAsync(command.adminId);

        if (user == null)
        {
            throw new ArgumentException("User ot found");
        }

        if (!user.Result.IsAdmin)
        {
            throw new Exception("User not an admin");
        }

        lot.Accept();
        await _lotRepository.SaveLotAsync(lot, cancellationToken);
    }
}
