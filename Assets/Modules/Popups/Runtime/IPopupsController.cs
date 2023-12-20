namespace PresentationModel.Popups
{
    public interface IPopupsController<TPopupBase>
    {
        TPopup Open<TPopup>(bool skipAnimation)
            where TPopup : TPopupBase;

        void Close<TPopup>(bool skipAnimation)
            where TPopup : TPopupBase;

        void CloseAll(bool skipAnimation);
        
        bool TryGetOpenedPopup<TPopup>(out TPopup popup)
            where TPopup : TPopupBase;
    }
}
