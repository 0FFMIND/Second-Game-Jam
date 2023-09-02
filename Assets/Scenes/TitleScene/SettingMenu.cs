
public class SettingMenu : TitleMenuBase<SettingMenu>
{
    public SelectButton selectButton;
    public override void OnBackPressed()
    {
        gameObject.GetComponentInParent<TitleController>().mainBackground.SetActive(true);
        gameObject.GetComponentInParent<TitleController>().subBackground.SetActive(false);
        Close();
    }
}
