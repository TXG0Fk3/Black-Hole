namespace Black_Hole.Enums
{
    public enum PendingType
    {
        SenderWaitingReceiverAcceptance, // Indica que o remetente está aguardando que o receptor aceite o envio
        ReceiverRespondingToConfirmation // Indica que o receptor está respondendo a confirmação (aceitando ou negando)
    }
}