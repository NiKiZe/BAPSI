namespace BapsiNet.Packet;

/// <summary>From apicommon.h</summary>
internal static class ApiCommonH
{
    //Data classes
    internal const byte MCLASS_COMMON = 0x30;
    internal const byte COMMON_TYPE_ADD_AND_UPDATE = 0x30;
    internal const byte COMMON_TYPE_REMOVE_CARD = 0x31;
    internal const byte COMMON_TYPE_SET_ACTIVE_STATE_ON_CARD = 0x32;
    internal const byte COMMON_TYPE_CHANGE_CARD_AUTHORITY = 0x34;
    internal const byte COMMON_TYPE_CLEAR_ALL_BAPSI_CARDS = 0x35;
    internal const byte COMMON_TYPE_CLEAR_ALL_BAPSI_USER_CARDS = 0x36;

    internal const byte MCLASS_APPLICATION = 0x31;
    internal const byte APPLICATION_TYPE_CLEAR_RESERVATIONS = 0x30;
    internal const byte APPLICATION_TYPE_ADD_RESERVATION = 0x31;
    internal const byte APPLICATION_TYPE_GET_RESERVATION = 0x32;

    internal const byte APPLICATION_TYPE_OPEN_CARD_EVENTS = 0x33;
    internal const byte APPLICATION_TYPE_READ_CARD_LOG_FROM_SEQ = 0x34;
    internal const byte APPLICATION_TYPE_READ_CARD_LOG_FROM_DATE = 0x35;
    internal const byte APPLICATION_TYPE_CLOSE_CARD_EVENTS = 0x36;

    internal const byte APPLICATION_TYPE_OPEN_GENERAL_EVENTS = 0x37;
    internal const byte APPLICATION_TYPE_CLOSE_GENERAL_EVENTS = 0x38;
    internal const byte APPLICATION_TYPE_READ_GENERAL_EVENTS_LOG_FROM_DATE = 0x39;

    ///////////////////////////////////
    internal const byte APPLICATION_TYPE_OPEN_ALAREA_STATUS_STREAM = 0x40;   //Open alarea status stream
    internal const byte APPLICATION_TYPE_CLOSE_ALAREA_STATUS_STREAM = 0x41;  //Close alarea status stream
    internal const byte APPLICATION_TYPE_GET_ALAREA_STATUS = 0x42;           //Get alarea status
    internal const byte APPLICATION_TYPE_GET_ALAREA_NAME = 0x43;


    internal const byte APPLICATION_TYPE_OPEN_ALGROUP_STATUS_STREAM = 0x44;  //Open algroup status stream
    internal const byte APPLICATION_TYPE_CLOSE_ALGROUP_STATUS_STREAM = 0x45; //Close algroup status stream
    internal const byte APPLICATION_TYPE_GET_ALGROUP_STATUS = 0x46;          //Get algroup status
    internal const byte APPLICATION_TYPE_GET_ALGROUP_NAME = 0x47;


    internal const byte APPLICATION_TYPE_OPEN_DOOR_STATUS_STREAM = 0x48;    //Open door status stream
    internal const byte APPLICATION_TYPE_CLOSE_DOOR_STATUS_STREAM = 0x49;   //Close door status stream
    internal const byte APPLICATION_TYPE_GET_DOOR_STATUS = 0x4a;            //Get door status
    internal const byte APPLICATION_TYPE_GET_DOOR_NAME = 0x4b;
    ///////////////////////////////////

    internal const byte MCLASS_READONLY = 0x32;
    internal const byte READONLY_TYPE_READ_ALL_CARDS = 0x30;
    internal const byte READONLY_TYPE_READ_CARDS_BY_SELECTION = 0x31;
    internal const byte READONLY_TYPE_READ_CARDS_BY_DATE_SELECTION = 0x32;
    internal const byte READONLY_TYPE_READ_NODES = 0x33;

    internal const byte MCLASS_DATASTREAM = 0x60;
    internal const byte DATASTREAM_TYPE_CARD_EVENTS = 0x32;
    internal const byte DATASTREAM_TYPE_GENERAL_EVENTS = 0x30;
    /*
    1	Action	0=Delete 1=change  2=Add
    2	Event index	Identifier for event type
    3	Node index	Identifier for node
    4	Localnet index	Identifier for localnet
    5	Report index	Identifier for report
    6	Altime
    7	Event name	Name for event type
    8	Node name	Name for node
    9	Localnet name	Name for localnet
    10	Event data name	Name for ??or data
    11	Report group name	Name for Report group
    */
    //internal const byte  DATASTREAM_TYPE_DOOR_EVENTS			=	0x32;

    internal const byte DATASTREAM_TYPE_STATUS_ALAREA = 0x34;         /* Report alarm area status from rbserver  */
    internal const byte DATASTREAM_TYPE_STATUS_ALGROUP = 0x33;        /* Report alarm group status from rbserver */
    internal const byte DATASTREAM_TYPE_STATUS_DOOR = 0x31;           /* Report door status from rbserver        */
    internal const byte DATASTREAM_TYPE_STATUS_NODE = 0x35;           /* Report node status from rbserver        */
    internal const byte DATASTREAM_TYPE_STATUS_CTLCHAN = 0x36;


    internal const byte MCLASS_RESPONSES = 0x61;
    internal const byte RESPONSES_TYPE_ACKNOWLEDGE = 0x06;
    //data code
    internal const short RESPONSES_OK = 0;
    internal const short RESPONSES_UNKNOWE_USER = 100;
    internal const short RESPONSES_USER_NOT_ALLOWED_TO_LOGIN = 110;
    internal const short RESPONSES_ALREADY_LOGGED_IN = 120;
    internal const short RESPONSES_NOT_LOGGED_IN = 130;
    internal const short RESPONSES_NO_SUCH_COMMAND = 200;
    internal const short RESPONSES_NOT_ALLOWED_COMMAND = 210;
    internal const short RESPONSES_SERVER_NOT_READY = 300;
    internal const short RESPONSES_SERVER_ENCRYPTION_ERROR = 310;
    internal const short RESPONSES_NO_SUCH_CARDCODE = 400;

    internal const short RESPONSES_DATA_IS_NOT_A_DATE = 600;
    internal const short RESPONSES_DATE_HAS_EXPIRED = 601;
    internal const short RESPONSES_WRONG_SEQ_NUMBER = 602;
    internal const short RESPONSES_DATA_OBJECT_UNKNOWE = 603;
    internal const short RESPONSES_DATAITEM_ERROR = 604;

    /*
            //internal const byte  RESPONSES_TYPE_RESERVATION				=		0x30;
            internal const byte  RESPONSES_TYPE_END_OF_RESERVATION			=	0x31;
            //internal const byte  RESPONSES_TYPE_WAIT						=		0x32;
            internal const byte  RESPONSES_TYPE_SERVER_RESPONS				=	0x33;
            internal const byte  RESPONSES_TYPE_BAPSI_CARDS_CLEARED			=	0x34; //bort
            internal const byte  RESPONSES_TYPE_RESERVATIONS_CLEARED		=		0x35;
            internal const byte  RESPONSES_TYPE_END_OF_CARD_EVENTS			=	0x36; //bort
            internal const byte  RESPONSES_TYPE_END_OF_ALQUEUE_LOG			=	0x37; //bort
            internal const byte  RESPONSES_TYPE_END_OF_CARD_DATA			=		0x38; //bort
    */
    //################################################################
    internal const byte RESPONSES_TYPE_END_OF_DATA = 0x31;
    internal const byte RESPONSES_TYPE_SERVER_RESPONS = 0x33;
    internal const byte RESPONSES_TYPE_OPERATION_DONE = 0x34;
    internal const byte RESPONSES_TYPE_RESERVATIONS_CLEARED = 0x35;
    //################################################################

    internal const byte MCLASS_MULTIPLE_RESPONSES = 0x62;
    internal const byte MULTIPLE_RESPONSES_CARD_DATA = 0x30;
    internal const byte MULTIPLE_RESERVATION = 0x31;

    internal const byte MCLASS_SYSTEM = 0x35;
    internal const byte SYSTEM_TYPE_LOGIN = 0x30;
    internal const byte SYSTEM_TYPE_LOGOUT = 0x31;
    internal const byte SYSTEM_TYPE_SERVER_REQUEST = 0x32;
    internal const byte SYSTEM_TYPE_CONNECTION_TERMINATED = 0x33;
    internal const byte SYSTEM_TYPE_PING = 0x36;
}
