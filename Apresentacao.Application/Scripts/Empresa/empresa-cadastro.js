$(function () {
    cadastroEmpresa.init();
});

var cadastroEmpresa = {

    init: function () {
        cadastroEmpresa.bind();
        cadastroEmpresa.mask();
        cadastroEmpresa.buscarEmpresas($("#pesquisar-nome").val(), 1);
    }

    , mask: function () {
        $("#cnpj").mask("99.999.999/9999-99");
    }

    , bind: function () {

        $(".salvar-cadastro").on("click", function (event) {
            if (cadastroEmpresa.validar()) {
                cadastroEmpresa.salvar();
            }
        });

        $("input[type='text'], textarea, input[type='password']").on("keydown", function () {
            if ($(this).parent(".input-group").next(".error").length > 0) {
                $(this).parent(".input-group").next(".error").empty();
                $(this).parent(".input-group").next(".error").hide("fast");
            }
        });

        $("#website").on('click focus', function(e) {
            if ($.trim($(e.target).val()) == '') $(e.target).val('http://');
        });        

        $(".paginas").find("a").die("click").live("click", function (e) {
            cadastroEmpresa.buscarEmpresas($("#pesquisar-nome").val(), $(this).parent("li").attr("rel"));
            e.stopPropagation();
            e.preventDefault();
        });
        
        $(".pesquisar-empresa").die("click").live("click", function (e) {
            cadastroEmpresa.buscarEmpresas($("#pesquisar-nome").val(), 1);
            e.stopPropagation();
            e.preventDefault();
        });

        $(".alterar").die("click").live("click", function (e) {

            var configuracao =
            {
                IdEmpresa: $(this).parents("tr").attr("rel"),
                Nome: $(this).parents("tr").eq(0).find(".nome").html(),
                NomeFantasia: $(this).parents("tr").eq(0).find(".nome-fantasia").html(),
                Cnpj: $(this).parents("tr").eq(0).find(".cnpj").html(),
                WebSite: $(this).parents("tr").eq(0).find(".website").html()
            };

            cadastroEmpresa.AtribuirValor(configuracao);

            e.stopPropagation();
            e.preventDefault();
        });

        $(".contatos").die("click").live("click", function (e) {
            var idEmpresa = $(this).parents("tr").eq(0).attr("rel");
            window.location = baseUrl + "Contato/Cadastrar/?idEmpresa=" + idEmpresa;
            e.stopPropagation();
            e.preventDefault();
        });
    }

    , AtribuirValor : function (data){
        $("#id-empresa").val(data.IdEmpresa);
        $("#nome").val(data.Nome);
        $("#nome-fantasia").val(data.NomeFantasia);
        $("#cnpj").val(data.Cnpj);
        $("#website").val(data.WebSite);
    }

    , recuperarDados: function () {
        var cnpj = $("#cnpj").val().replace(/\./g, "").replace(/\//g, "").replace(/\-/g, "");
        
        var configuracao =
        {
            IdEmpresa: $("#id-empresa").val(),
            Nome: $("#nome").val(),
            NomeFantasia: $("#nome-fantasia").val(),
            Cnpj: cnpj,
            WebSite: $("#website").val()
        };

        return configuracao;
    }

    , validar: function () {
        var retorno = true;
        var dados = cadastroEmpresa.recuperarDados();

        if (dados.Nome == "") {
            $(".error.nome").html("Informe um nome");
            $(".error.nome").show("fast");
            retorno = false;
        }

        if (dados.NomeFantasia == "") {
            $(".error.nome-fantasia").html("Informe um nome fantasia");
            $(".error.nome-fantasia").show("fast");
            retorno = false;
        }

        if (dados.Cnpj == "") {
            $(".error.cnpj").html("Informe um CNPJ");
            $(".error.cnpj").show("fast");
            retorno = false;
        }
        
        if (dados.WebSite == "") {
            $(".error.website").html("Informe um website");
            $(".error.website").show("fast");
            retorno = false;
        }
        
        return retorno;
    }

    , salvar: function () {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Empresa/SalvarCadastro",
            data: { configuracao: JSON.stringify(cadastroEmpresa.recuperarDados()), r: (new Date().getTime()) },
            async: false,
            success: function (data) {
                if (data) {
                    jAlert('Salvo com sucesso!', 'Atenção', function () {
                        //window.location = baseUrl + "Home/Index";
                    });
                }
                else {
                    jAlert('Não foi possível salvar!', 'Atenção');
                }
            },
            failure: function (msg, status) {
                jAlert('Não foi possível salvar!', 'Atenção');
            },
            error: function (msg, status) {
                jAlert('Não foi possível salvar!', 'Atenção');
            },
            complete: function () {
            }
        });
    }

    , buscarEmpresas: function (nome, page) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Empresa/ListaEmpresas",
            data: { nome: nome, page: page,  r: (new Date().getTime()) },
            async: false,
            success: function (data) {
                $(".table.empresas tbody tr").empty();
                $(".pagination").empty();

                if (data.Pesquisa.length > 0)
                {
                    var render = $('#empresa-pesquisa-template').render(data.Pesquisa);
                    $(".table.empresas").find("tbody").append(render);
                    
                    // -- Paginação -----------------------
                    $(".pagination").append("<li><a href='#'>«</a></li>");
                    for (var i = 1; i <= data.TotalPagina; i++) {
                        var renderPaginacao = $('#paginacao-template').render({ pagina: i, className: (page == i ? 'active' : '') });
                        $(".pagination").append(renderPaginacao);
                    }
                    $(".pagination").append("<li><a href='#'>»</a></li>");

                } else {
                    var render = $('#empresa-pesquisa-sem-registro-template').render({ Nome: "Sem registro!" });
                    $(".table.empresas").find("tbody").append(render);
                }
            }
        });
    } 
};
