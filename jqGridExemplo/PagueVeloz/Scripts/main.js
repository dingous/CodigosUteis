

function MontaGridFornecedores() {

	$("#jqGridFornecedor").jqGrid({
		url: '/Fornecedor/Listar',
		datatype: "json",
		styleUI: 'Bootstrap',
		colModel: [
			{ label: 'Código', name: 'Codigo', key: true, width: 60 },
			{ label: 'CPF / CNPJ', name: 'CNPJ', width: 150 },
			{ label: 'Nome Fornecedor', name: 'Nome', width: 180 },
			{ label: 'Tipo Pessoa', name: 'TipoPessoa', width: 130 },
			{ label: 'Empresa', name: 'Empresa', width: 150 },
			//{ label: 'Telefones', name: 'Telefone', width: 150 }
		],
		viewrecords: true, // show the current page, data rang and total records on the toolbar
		width: 780,
		height: 200,
		rowNum: 100000,
		pager: "#jqGridPagerFornecedor"
	});
}


$(document).ready(function () {

	
	$.jgrid.defaults.width = 780;

	$("#jqGrid").jqGrid({
		url: '/Empresa/Listar',
		datatype: "json",
		styleUI: 'Bootstrap',
		postData: { "filtro": "teste" },
		colModel: [
			{ label: 'Código', name: 'Codigo', key: true, width: 75 },
			{ label: 'CNPJ', name: 'CNPJ', width: 150 },
			{ label: 'Nome Fantasia', name: 'NomeFantasia', width: 150 },
			{ label: 'Estado', name: 'Sigla', width: 150 }
		],
		viewrecords: true, // show the current page, data rang and total records on the toolbar
		width: 780,
		height: 200,
		rowNum: 100000,
		pager: "#jqGridPager"
	});


	MontaGridFornecedores();

	$("#BtnFiltrar").click(function () {		

		jQuery("#jqGridFornecedor").jqGrid().setGridParam({ url: '/Fornecedor/Listar?filtro=' + $("#filtro").val() + "&filtroDataCadastro=" + $("#filtroDataCadastro").val() });

		jQuery("#jqGridFornecedor").trigger("reloadGrid");

	});

	$("#btnAddTelefone").click(function () {

		var clone = $("#modeloToClone").clone();

		clone.find("input[type='button']").remove();

		

		var size = $("#clones").find(".row").length + 1;

		clone.find("#TEL_DDD").attr("name", "TELEFONE[" + size + "].TEL_DDD");

		clone.find("#TEL_Numero").attr("name", "TELEFONE[" + size+"].TEL_Numero");

		clone.appendTo("#clones");
	});

	$(function () {
		$.validator.methods.date = function (value, element) {
			return this.optional(element) || moment(value, "DD.MM.YYYY", true).isValid();
		}
	});

	$('.datepicker').datepicker({
		format: 'dd/mm/yyyy',
		language: 'pt-BR'
	});
});
