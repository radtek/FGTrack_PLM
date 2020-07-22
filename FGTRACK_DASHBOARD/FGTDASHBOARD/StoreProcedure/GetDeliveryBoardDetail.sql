SELECT
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
    MAX(FLAG)
FROM DELIVERY_DETAIL
WHERE  (PARTY_ID = @strPARTY_ID) AND (ETD_DATE = @strETD_DATE)
GROUP BY PARTY_ID,
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
         REMARK
;

