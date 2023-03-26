using System.ComponentModel;
using SalesPitch.TypeConverters;

namespace SalesPitch.Commands;

[TypeConverter(typeof(SalesPitchFrameworkConverter))]
public enum SalesPitchFramework
{
    /// <summary>
    ///     AIDA (Attention, Interest, Desire, Action)
    ///     A formula for writing persuasive copy that aims to grab the reader’s attention, build interest, create desire, and prompt action.
    /// </summary>
    /// <remarks>
    ///     * Luxury vacations or travel packages
    ///     * Fitness equipment and wellness products
    ///     * High-end electronics or gadgets
    ///     * Personal development courses or books
    /// </remarks>
    AIDA,
    
    /// <summary>
    ///     PAS (Problem-Agitate-Solve)
    ///     A format that identifies the reader’s problem agitates the issue and provides a solution.
    /// </summary>
    /// <remarks>
    ///     * Home security systems
    ///     * Pain relief products or medications
    ///     * Debt relief services
    ///     * Car repair or maintenance services
    /// </remarks>
    PAS,
    
    /// <summary>
    ///     USP (Unique Selling Proposition)
    ///     A statement that clearly communicates what makes a product or service unique and valuable to the target audience.
    /// </summary>
    /// <remarks>
    ///     * Speciality or niche products
    ///     * High-end or luxury goods
    ///     * Personalized or customized products
    ///     * Products with unique or proprietary technology
    /// </remarks>
    USP,
    
    /// <summary>
    ///     Features-Benefits
    ///     A format that lists the features of a product or service and then explains the benefits those features provide to the reader.
    /// </summary>
    /// <remarks>
    ///     * Home appliances or kitchen gadgets
    ///     * Personal grooming or beauty products
    ///     * Automobiles
    ///     * Office equipment or technology products
    /// </remarks>
    FeaturesBenefits,
    
    /// <summary>
    ///     Storytelling
    ///     A format that uses a story to engage the reader and convey a message memorably and emotionally.
    /// </summary>
    /// <remarks>
    ///     * Charitable organizations or non-profits
    ///     * Personal growth or motivational products
    ///     * Eco-friendly or sustainable products
    ///     * Adventure travel or outdoor recreation products
    /// </remarks>
    Storytelling,
    
    /// <summary>
    ///     WIIFM (What’s In It For Me)
    ///     A principle of copywriting that emphasizes the reader’s perspective. It emphasizes the benefits they will receive from a product or service.
    /// </summary>
    /// <remarks>
    ///     * Self-help or personal development products
    ///     * Career development or job search services
    ///     * Investment or financial planning services
    ///     * Health and wellness products or services
    /// </remarks>
    WIIFM,
    
    /// <summary>
    ///     Youtility
    ///     A marketing approach that focuses on delivering highly useful information to customers rather than just promoting products.
    /// </summary>
    /// <remarks>
    ///     * Technical or software products
    ///     * Health and wellness products or services
    ///     * Personal finance or investment products
    ///     * Home improvement or DIY products
    /// </remarks>
    Youtility,
    
    /// <summary>
    ///     FAB (Features, Advantages, Benefits)
    ///     A copywriting formula that lists the features of a product, explains the advantages of those features and then highlights the benefits to the reader.
    /// </summary>
    /// <remarks>
    ///     * Home improvement or remodelling services
    ///     * Fitness or wellness products
    ///     * Personal development courses or books
    ///     * Technology or software products
    /// </remarks>
    FAB,
    
    /// <summary>
    ///     HHE (Headline, Hook, Empathy)
    ///     A formula for writing effective headlines that grab the reader’s attention, deliver a compelling hook and create a connection with the reader by showing empathy.
    /// </summary>
    /// <remarks>
    ///     * Personal growth or motivational products
    ///     * Health and wellness products or services
    ///     * Debt relief services
    ///     * Career development or job search services
    /// </remarks>
    HHE,
    
    /// <summary>
    ///     SUSPENSE (Surprise, Uniqueness, Specifics, Promise, Excitement, Newness, Story)
    ///     A copywriting formula that aims to create suspense by surprising the reader, emphasizing the uniqueness of a product or service, providing specific details, making a promise, generating excitement, highlighting newness, and telling a story.
    /// </summary>
    /// <remarks>
    ///     * Thrill-seeking experiences or adventure travel
    ///     * Cutting-edge technology or gadgets
    ///     * High-end fashion or luxury products
    ///     * Next-generation gaming or entertainment products
    /// </remarks>
    SUSPENSE
}