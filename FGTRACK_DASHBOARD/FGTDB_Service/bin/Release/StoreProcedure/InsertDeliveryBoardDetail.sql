INSERT INTO DELIVERY_DETAIL ( 
    PARTY_ID,
    WH_ID,
    ETD_DATE,
    PRODUCT_NO,
    PRODUCT_NAME,
    QTY,
    UNIT,
    NO_OF_BOX,
    FREE_STOCK,
    ASSIGN_QTY,
    PICKED_QTY,
    LOADED_QTY,
    STATUS,
    REMARK,
    FLAG,
    LASTUPDATE
) 
VALUES ( 
    @PARTY_ID,
    @WH_ID,
    @ETD_DATE,
    @PRODUCT_NO,
    @PRODUCT_NAME,
    @QTY,
    @UNIT,
    @NO_OF_BOX,
    @FREE_STOCK,
    @ASSIGN_QTY,
    @PICKED_QTY,
    @LOADED_QTY,
    @STATUS,
    @REMARK,
    @FLAG,
    @LASTUPDATE
);

