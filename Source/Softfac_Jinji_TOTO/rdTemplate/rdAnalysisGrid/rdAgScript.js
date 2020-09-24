
function rdAgShowChartAdd(sChartType) {
	rdForm.rdAgChartType.value=sChartType;
	ShowElement(this.id,'divChartAdd','Show');
	switch (sChartType) {
			case 'Pie':
			case 'Bar':
				ShowElement(this.id,'lblChartXLabelColumn','Show');
				ShowElement(this.id,'lblChartXAxisColumn','Hide');
				ShowElement(this.id,'lblChartYDataColumn','Show');
				ShowElement(this.id,'lblChartYAxisColumn','Hide');
				ShowElement(this.id,'rdAgChartXLabelColumn','Show');
				ShowElement(this.id,'rdAgChartXDataColumn','Hide');
				ShowElement(this.id,'rdAgChartXNumberColumn','Hide');
				ShowElement(this.id,'rdAgChartYAggrLabel','Show');
				ShowElement(this.id,'rdAgChartYAggrList','Show');
				break;
			case 'Scatter':
				ShowElement(this.id,'lblChartXLabelColumn','Hide');
				ShowElement(this.id,'lblChartXAxisColumn','Show');
				ShowElement(this.id,'lblChartYDataColumn','Hide');
				ShowElement(this.id,'lblChartYAxisColumn','Show');
				ShowElement(this.id,'rdAgChartXLabelColumn','Hide');
				ShowElement(this.id,'rdAgChartXDataColumn','Hide');
				ShowElement(this.id,'rdAgChartXNumberColumn','Show');
				ShowElement(this.id,'rdAgChartYAggrLabel','Hide');
				ShowElement(this.id,'rdAgChartYAggrList','Hide');
				break;
			default:
				ShowElement(this.id,'lblChartXLabelColumn','Hide');
				ShowElement(this.id,'lblChartXAxisColumn','Show');
				ShowElement(this.id,'lblChartYDataColumn','Hide');
				ShowElement(this.id,'lblChartYAxisColumn','Show');
				ShowElement(this.id,'rdAgChartXLabelColumn','Hide');
				ShowElement(this.id,'rdAgChartXDataColumn','Show');
				ShowElement(this.id,'rdAgChartXNumberColumn','Hide');
				ShowElement(this.id,'rdAgChartYAggrLabel','Hide');
				ShowElement(this.id,'rdAgChartYAggrList','Hide');
				break;
	}
}

function rdAgColumnMove(sID, nRow, nDirection) {
	var eleThis = document.getElementById('lblColIndent_Row' + nRow)
	var sThisIndent = eleThis.innerHTML
	var sOtherIndent
	if (nDirection<0) {
		if (sThisIndent.length==0) {
			return
		}
		sOtherIndent = sThisIndent.substr(0,sThisIndent.length-1)  //Take off one ".".
	} else {
		sOtherIndent = sThisIndent + '.'
	}
	//Find the Other column with the new indent.
	var eleOther
	var eleTest
	var i
	for (i=1; i<=100; i++) {
		eleTest = document.getElementById('lblColIndent_Row' + i)
		if (!eleTest) {
			break
		}
		if (eleTest.innerHTML==sOtherIndent) {
			eleOther = eleTest
			break
		}
	}
	
	//Switch indents.
	if (eleOther) {
		var sThis = eleThis.innerHTML 
		var sOther = eleOther.innerHTML 
		eleThis.innerHTML = sOther
		eleOther.innerHTML = sThis
		
		//Save the indent so it goes back to the server.
		var eleColMoves = document.getElementById('rdAgColMoves')
		eleColMoves.value = eleColMoves.value + sID + ',' + nDirection + ';'
	}
	
}

