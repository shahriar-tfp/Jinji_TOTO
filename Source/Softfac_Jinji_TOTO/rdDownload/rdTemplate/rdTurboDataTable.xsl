<TableRows>
	<xsl:for-each select="/*/rdDataID" >
    <rdHeaderRow />
		<xsl:variable name="rdDataTableID-Position" select="position()"/>
		<rdTableRows />
	</xsl:for-each>
</TableRows>
