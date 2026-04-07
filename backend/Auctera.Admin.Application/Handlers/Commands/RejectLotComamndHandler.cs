using Auctera.Admin.Application.Commands;
using Auctera.Identity.Application.Interfaces;
using Auctera.Items.Application.Interfaces;

using MediatR;

namespace Auctera.Admin.Application.Handlers.Commands;

public sealed class RejectLotComamndHandler : IRequestHandler<RejectLotCommand>
{
    private readonly ILotRepository _lotRepository;
    private readonly IUserRepository _userRepository;

    public RejectLotComamndHandler(ILotRepository lotRepository, IUserRepository userRepository)
    {
        _lotRepository = lotRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(RejectLotCommand command, CancellationToken cancellationToken)
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

        lot.Reject();
        await _lotRepository.SaveLotAsync(lot,cancellationToken);
    }
}
