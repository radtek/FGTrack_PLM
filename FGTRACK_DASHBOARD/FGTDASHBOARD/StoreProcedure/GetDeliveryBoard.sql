SELECT
    PARTY_ID,
    PARTY_NAME,
    WH_ID,
    ETD_DATE,
    ETD_TIME,
    STATUS,
    RESPONSIBLE,
    MAX(FLAG)
FROM DELIVERY_BOARD
GROUP BY PARTY_ID,
         PARTY_NAME,
         WH_ID,
         ETD_DATE,
         ETD_TIME,
         STATUS,
         RESPONSIBLE
;

