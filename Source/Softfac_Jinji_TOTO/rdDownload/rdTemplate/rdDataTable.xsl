
<xsl:variable name="nLastPageNr" select="rdXslExtension:GetTokenValue('@Session.rdElementID-LastPageNr~')" />
<xsl:variable name="nPageRowCnt" select="rdXslExtension:GetTokenValue('@Session.rdElementID-PageRowCnt~')" />
<xsl:variable name="nPageNr" select="rdXslExtension:GetTokenValue('@Session.rdElementID-PageNr~')" />
<xsl:variable name="nFirstRow" select="$nPageRowCnt * ($nPageNr - 1) + 1" />
<xsl:variable name="nLastRow" select="$nFirstRow + $nPageRowCnt - 1" />

<TableRows>
	<xsl:for-each select="/*/rdDataID" >
    <xsl:variable name="rdDataTableID-Position" select="position()"/>
    <xsl:if test="position() &gt;= $nFirstRow and position() &lt;= $nLastRow">
      <rdTableRows />
    </xsl:if>
    <rdRowEnd />
	</xsl:for-each>
</TableRows>
