namespace BapsiNet.Packet;

/// <summary>
/// This is a combination enum of the official separate Data class (mclass) and Data Type (type) codes.
/// Combined enum makes it easier to handle type meaning
/// </summary>
public enum DataClassType : ushort
{
    Undefined = 0x0000,

    _Common = ApiCommonH.MCLASS_COMMON << 8,
    CommonAddAndUpdate = _Common | ApiCommonH.COMMON_TYPE_ADD_AND_UPDATE,
    CommonRemoveCard = _Common | ApiCommonH.COMMON_TYPE_REMOVE_CARD,
    CommonSetActiveStateOnCard = _Common | ApiCommonH.COMMON_TYPE_SET_ACTIVE_STATE_ON_CARD,
    CommonChangeCardAuthority = _Common | ApiCommonH.COMMON_TYPE_CHANGE_CARD_AUTHORITY,
    CommonClearAllBapsiCards = _Common | ApiCommonH.COMMON_TYPE_CLEAR_ALL_BAPSI_CARDS,
    CommonClearAllBapsiUserCards = _Common | ApiCommonH.COMMON_TYPE_CLEAR_ALL_BAPSI_USER_CARDS,

    _Application = ApiCommonH.MCLASS_APPLICATION << 8,
    ApplicationClearReservations = _Application | ApiCommonH.APPLICATION_TYPE_CLEAR_RESERVATIONS,
    ApplicationAddReservation = _Application | ApiCommonH.APPLICATION_TYPE_ADD_RESERVATION,
    ApplicationGetReservation = _Application | ApiCommonH.APPLICATION_TYPE_GET_RESERVATION,

    ApplicationOpenCardEvents = _Application | ApiCommonH.APPLICATION_TYPE_OPEN_CARD_EVENTS,
    ApplicationReadCardLogFromSeq = _Application | ApiCommonH.APPLICATION_TYPE_READ_CARD_LOG_FROM_SEQ,
    ApplicationReadCardLogFromDate = _Application | ApiCommonH.APPLICATION_TYPE_READ_CARD_LOG_FROM_DATE,
    ApplicationCloseCardEvents = _Application | ApiCommonH.APPLICATION_TYPE_CLOSE_CARD_EVENTS,

    ApplicationOpenGeneralEvents = _Application | ApiCommonH.APPLICATION_TYPE_OPEN_GENERAL_EVENTS,
    ApplicationCloseGeneralEvents = _Application | ApiCommonH.APPLICATION_TYPE_CLOSE_GENERAL_EVENTS,
    ApplicationReadGeneralEventsLogFromDate = _Application | ApiCommonH.APPLICATION_TYPE_READ_GENERAL_EVENTS_LOG_FROM_DATE,

    ApplicationOpenAlAreaStatusStream = _Application | ApiCommonH.APPLICATION_TYPE_OPEN_ALAREA_STATUS_STREAM,   //Open alarea status stream		
    ApplicationCloseAlAreaStatusStream = _Application | ApiCommonH.APPLICATION_TYPE_CLOSE_ALAREA_STATUS_STREAM,  //Close alarea status stream		
    ApplicationGetAlAreaStatus = _Application | ApiCommonH.APPLICATION_TYPE_GET_ALAREA_STATUS,           //Get alarea status		
    ApplicationGetAlAreaName = _Application | ApiCommonH.APPLICATION_TYPE_GET_ALAREA_NAME,

    ApplicationOpenAlGroupStatusStream = _Application | ApiCommonH.APPLICATION_TYPE_OPEN_ALGROUP_STATUS_STREAM,  //Open algroup status stream		
    ApplicationCloseAlGroupStatusStream = _Application | ApiCommonH.APPLICATION_TYPE_CLOSE_ALGROUP_STATUS_STREAM, //Close algroup status stream		
    ApplicationGetAlGroupStatus = _Application | ApiCommonH.APPLICATION_TYPE_GET_ALGROUP_STATUS,         //Get algroup status
    ApplicationGetAlGroupName = _Application | ApiCommonH.APPLICATION_TYPE_GET_ALGROUP_NAME,

    ApplicationOpenDoorStatusStream = _Application | ApiCommonH.APPLICATION_TYPE_OPEN_DOOR_STATUS_STREAM,    //Open door status stream		
    ApplicationCloseDoorStatusStream = _Application | ApiCommonH.APPLICATION_TYPE_CLOSE_DOOR_STATUS_STREAM,   //Close door status stream		
    ApplicationGetDoorStatus = _Application | ApiCommonH.APPLICATION_TYPE_GET_DOOR_STATUS,            //Get door status
    ApplicationGetDoorName = _Application | ApiCommonH.APPLICATION_TYPE_GET_DOOR_NAME,

    _ReadOnly = ApiCommonH.MCLASS_READONLY << 8,
    ReadOnlyReadAllCards = _ReadOnly | ApiCommonH.READONLY_TYPE_READ_ALL_CARDS,
    ReadOnlyReadCardsBySelection = _ReadOnly | ApiCommonH.READONLY_TYPE_READ_CARDS_BY_SELECTION,
    ReadOnlyReadCardsByDateSelection = _ReadOnly | ApiCommonH.READONLY_TYPE_READ_CARDS_BY_DATE_SELECTION,
    ReadOnlyReadNodes = _ReadOnly | ApiCommonH.READONLY_TYPE_READ_NODES,

    _System = ApiCommonH.MCLASS_SYSTEM << 8,
    SystemLogin = _System | ApiCommonH.SYSTEM_TYPE_LOGIN,
    SystemLogout = _System | ApiCommonH.SYSTEM_TYPE_LOGOUT,
    SystemServerRequest = _System | ApiCommonH.SYSTEM_TYPE_SERVER_REQUEST,
    SystemConnectionTerminated = _System | ApiCommonH.SYSTEM_TYPE_CONNECTION_TERMINATED,
    SystemPing = _System | ApiCommonH.SYSTEM_TYPE_PING,

    /// <summary><see cref="DataTypeDataStream"/></summary>
    _DataStream = ApiCommonH.MCLASS_DATASTREAM << 8,
    DataStreamCardEvents = _DataStream | ApiCommonH.DATASTREAM_TYPE_CARD_EVENTS,
    DataStreamGeneralEvents = _DataStream | ApiCommonH.DATASTREAM_TYPE_GENERAL_EVENTS,
    DataStreamStatusAlArea = _DataStream | ApiCommonH.DATASTREAM_TYPE_STATUS_ALAREA,         /* Report alarm area status from rbserver  */
    DataStreamStatusAlGroup = _DataStream | ApiCommonH.DATASTREAM_TYPE_STATUS_ALGROUP,        /* Report alarm group status from rbserver */
    DataStreamStatusDoor = _DataStream | ApiCommonH.DATASTREAM_TYPE_STATUS_DOOR,           /* Report door status from rbserver        */
    DataStreamStatusNode = _DataStream | ApiCommonH.DATASTREAM_TYPE_STATUS_NODE,           /* Report node status from rbserver        */
    DataStreamStatusCtlChan = _DataStream | ApiCommonH.DATASTREAM_TYPE_STATUS_CTLCHAN,

    _SingleResponse = ApiCommonH.MCLASS_RESPONSES << 8,
    SingleResponseAck = _SingleResponse | ApiCommonH.RESPONSES_TYPE_ACKNOWLEDGE,
    SingleResponseEndOfData = _SingleResponse | ApiCommonH.RESPONSES_TYPE_END_OF_DATA,
    SingleResponseServerResponse = _SingleResponse | ApiCommonH.RESPONSES_TYPE_SERVER_RESPONS,
    SingleResponseOperationDone = _SingleResponse | ApiCommonH.RESPONSES_TYPE_OPERATION_DONE,
    SingleResponseReservationsCleared = _SingleResponse | ApiCommonH.RESPONSES_TYPE_RESERVATIONS_CLEARED,

    _MultipleResponse = ApiCommonH.MCLASS_MULTIPLE_RESPONSES << 8,
    MultipleResponseCardData = _MultipleResponse | ApiCommonH.MULTIPLE_RESPONSES_CARD_DATA,
    MultipleResponseReservation = _MultipleResponse | ApiCommonH.MULTIPLE_RESERVATION,
}

public static class DataClassTypeExtensions
{
    public static string String(this DataClassType dct) =>
        $"{(Enum.IsDefined(dct) ? dct : (dct & (DataClassType)0xff00) + "+")}(0x{(ushort)dct:x4})";
}
