namespace IDPService.Business.Gateways
{
    public class EventGridTopic
    {
        public string Endpoint { get; set; }
        public string Key { get; set; }
    }

    public enum EventTypeEnum
    {
        removeCategory,
        formRecognizerAnalyzeForm,
        formRecognizerAnalyzeInvoiceForm,
        AnalyzeReceipt,
        formRecognizerAnalyzeIdForm,
        formRecognizerTrainModel,
        sendMailInvitation
    }
}