INSERT INTO DELIVERY_BOARD ( 
    PARTY_ID,
    PARTY_NAME,
    WH_ID,
    ETD_DATE,
    ETD_TIME,
    STATUS,
    RESPONSIBLE,
    FLAG,
    LASTUPDATE 
) 
VALUES ( 
    @PARTY_ID,
    @PARTY_NAME,
    @WH_ID,
    @ETD_DATE,
    @ETD_TIME,
    @STATUS,
    @RESPONSIBLE,
    @FLAG,
    @LASTUPDATE
);

