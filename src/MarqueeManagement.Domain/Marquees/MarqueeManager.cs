using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MarqueeManagement.Marquees;

public class MarqueeManager : DomainService
{
    private readonly IMarqueeRepository _marqueeRepository;

    public MarqueeManager(IMarqueeRepository marqueeRepository)
    {
        _marqueeRepository = marqueeRepository;
    }
    public async Task<Marquee> CreateAsync(
        string name,
        string location,
        string? description,
        int capacity,
        decimal pricePerDay
        )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(location, nameof(location));

        var existingMarquee = await _marqueeRepository.FindByNameAsync(name);
        if (existingMarquee != null)
        {
            throw new MarqueeAlreadyExistsException(name);
        }
        return new Marquee(
            GuidGenerator.Create(),
            name,
            location,
            description,
            capacity,
            pricePerDay
            );
    }

    public async Task UpdateAsync(
        Marquee marquee,
        string name,
        string location,
        string? description,
        int capacity,
        decimal pricePerDay
        )
    {
        Check.NotNull(marquee, nameof(marquee));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(location, nameof(location));

        var existingMarquee = await _marqueeRepository.FindByNameAsync(name);
        if (existingMarquee != null && existingMarquee.Id != marquee.Id)
        {
            throw new MarqueeAlreadyExistsException(name);
        }

        marquee.ChangeName(name);
        marquee.ChangeLocation(location);
        marquee.ChangeDescription(description);
        marquee.ChangeCapacity(capacity);
        marquee.ChangePricePerDay(pricePerDay);
    }
}