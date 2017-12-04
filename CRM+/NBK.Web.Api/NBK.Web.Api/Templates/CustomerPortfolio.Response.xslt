<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
 xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"  xmlns:json='http://james.newtonking.com/projects/json'>
  <xsl:template match="/">
    <CustomerPortfolioResponse>
      <xsl:choose>
        <xsl:when test="//ResponseStatus">
          <ErrCode>
            <xsl:value-of select="//ResponseStatus/ResponseStatusCode/."/>
          </ErrCode>
          <ErrDesc>
            <xsl:value-of select="//ResponseStatus/ResponseStatusDescription/."/>
          </ErrDesc>
        </xsl:when>
        <xsl:otherwise>
          <ErrCode>0</ErrCode>
          <ErrDesc>OK</ErrDesc>
        </xsl:otherwise>
      </xsl:choose>

      <Products>
        <xsl:for-each select="soap:Envelope/soap:Body/BFX/Message/GetCustomerPortfolioRs/BankProdRec">
          <Product json:Array='true'>
            <CustomerNumber>
              <xsl:value-of select="./CustId/CustPermId/."/>
            </CustomerNumber>
            <ProductNumber>
              <xsl:value-of select="./BankProdId/ProdId/."/>
            </ProductNumber>
            <ProductName>
              <xsl:value-of select="./BankProdId/ProdType/Desc/."/>
            </ProductName>
            <AccountStatus>
              <xsl:value-of select="./BankProdId/ProdStatus/ProdStatusCode/Val/."/>
            </AccountStatus>
            <Code>
              <xsl:value-of select="./BankProdId/ProdType/Standard/."/>
            </Code>
            <ProductCategory>
              <xsl:value-of select="./BankProdId/ProdType/Category/."/>
            </ProductCategory>
            <ProductSubCategory>
              <xsl:value-of select="./BankProdId/ProdType/SubCategory/."/>
            </ProductSubCategory>
            <CurrencyCode>
              <xsl:value-of select="./BankProdId/ProdCur/CurCode/Val/."/>
            </CurrencyCode>
            <Balance>
              <xsl:value-of select="./ProdBal[BalType/. = 'Current']/CurAmt/Amt/."/>
            </Balance>
            <TransferStatus>
              <xsl:value-of select="./TransferInd/."/>
            </TransferStatus>
            <NumberExt>
              <xsl:for-each select="./BankProdIdExt">
                <xsl:value-of select="./ProdId/."/>
                <xsl:if test="not(position() = last())">
                  <xsl:text>:</xsl:text>
                </xsl:if>
              </xsl:for-each>
            </NumberExt>
          </Product>
        </xsl:for-each>
      </Products>
    </CustomerPortfolioResponse>
  </xsl:template>
</xsl:stylesheet>
