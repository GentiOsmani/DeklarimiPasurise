namespace DeklarimiPasuris.Models;

public class DeclarationModel
{
    public IncomeModel? IncomeModel { get; set; }
    public DonationModel? DonationModel { get; set; }
    public FinancialObligationModel? FinancialObligationModel { get; set; }
    public LiquidMoneyModel? LiquidMoneyModel { get; set; }
    public CryptoModel? CryptoModel { get; set; }
    public BondModel? BondModel { get; set; }
    public ActionsModel? ActionsModel { get; set; }
    public MovableAssetsModel? MovableAssetsModel { get; set; }
    public RealEstateModel? RealEstateModel { get; set; }
}