namespace BapsiNet.Packet;

/// <summary>Match code in <see cref="DataClassType.SingleResponseAck"/></summary>
public enum ResponseAckType : short
{
    Ok = ApiCommonH.RESPONSES_OK,
    UnknownUser = ApiCommonH.RESPONSES_UNKNOWE_USER,
    UserNotAllowedToLogin = ApiCommonH.RESPONSES_USER_NOT_ALLOWED_TO_LOGIN,
    AlreadyLoggedIn = ApiCommonH.RESPONSES_ALREADY_LOGGED_IN,
    NotLoggedIn = ApiCommonH.RESPONSES_NOT_LOGGED_IN,
    NoSuchCommand = ApiCommonH.RESPONSES_NO_SUCH_COMMAND,
    NotAllowedCommand = ApiCommonH.RESPONSES_NOT_ALLOWED_COMMAND,
    ServerNotReady = ApiCommonH.RESPONSES_SERVER_NOT_READY,
    ServerEncryptionRrror = ApiCommonH.RESPONSES_SERVER_ENCRYPTION_ERROR,
    NoSuchCardcode = ApiCommonH.RESPONSES_NO_SUCH_CARDCODE,
    DataIsNotADate = ApiCommonH.RESPONSES_DATA_IS_NOT_A_DATE,
    DateHasExpired = ApiCommonH.RESPONSES_DATE_HAS_EXPIRED,
    WrongSeqNumber = ApiCommonH.RESPONSES_WRONG_SEQ_NUMBER,
    DataObjectUnknown = ApiCommonH.RESPONSES_DATA_OBJECT_UNKNOWE,
    DataitemError = ApiCommonH.RESPONSES_DATAITEM_ERROR,
}
