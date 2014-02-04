$(function () {
    cadastroContato.init();
});

var cadastroContato = {

    init: function () {
        cadastroContato.bind();
        cadastroContato.mask();
        cadastroContato.buscarContatos($("#pesquisar-nome").val(),$("#id-empresa").val(), 1);
    }

    , mask: function () {
        
        $("#celular").mask("(99) 9999-9999?9").ready(function (event) {
            cadastroContato.mascaraTelefone(event);
        });
        
        $("#telefone").mask("(99) 9999-9999").ready(function (event) {
            cadastroContato.mascaraTelefone(event);
        });
    }

    , mascaraTelefone: function (event)
    {
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target);
        element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-999?9");
        } else {
            element.mask("(99) 9999-9999?9");
        }
    }

    , bind: function () {

        $(".salvar-cadastro").on("click", function (event) {
            if (cadastroContato.validar()) {
                cadastroContato.salvar();
            }
        });

        $("input[type='text'], textarea, input[type='password']").on("keydown", function () {
            if ($(this).parent(".input-group").next(".error").length > 0) {
                $(this).parent(".input-group").next(".error").empty();
                $(this).parent(".input-group").next(".error").hide("fast");
            }
        });

        $(".paginas").find("a").die("click").live("click", function (e) {
            cadastroContato.buscarEmpresas($("#pesquisar-nome").val(), $(this).parent("li").attr("rel"));
            e.stopPropagation();
            e.preventDefault();
        });

        $(".pesquisar-contatos").die("click").live("click", function (e) {
            cadastroContato.buscarContatos($("#pesquisar-nome").val(), $("#id-empresa").val(), 1);
            e.stopPropagation();
            e.preventDefault();
        });

        $(".alterar").die("click").live("click", function (e) {

            var configuracao =
            {
                IdEmpresa: $("#id-empresa").val(),
                IdContato: $(this).parents("tr").attr("rel"),
                Nome: $(this).parents("tr").eq(0).find(".nome").html(),
                Telefone: $(this).parents("tr").eq(0).find(".telefone").html(),
                Email: $(this).parents("tr").eq(0).find(".email").html(),
                Celular: $(this).parents("tr").eq(0).find(".celular").html()
            };

            cadastroContato.AtribuirValor(configuracao);

            e.stopPropagation();
            e.preventDefault();
        });

    }

    , AtribuirValor: function (data) {
        $("#id-contato").val(data.IdContato);
        $("#nome").val(data.Nome);
        $("#telefone").val(data.Telefone);
        $("#email").val(data.Email);
        $("#celular").val(data.Celular);
    }

    , recuperarDados: function () {

        var configuracao =
        {
            IdEmpresa: $("#id-empresa").val(),
            IdContato: $("#id-contato").val(),
            Nome: $("#nome").val(),
            Telefone: $("#telefone").val(),
            Email: $("#email").val(),
            Celular: $("#celular").val()
        };

        return configuracao;
    }

    , validar: function () {
        var retorno = true;
        var dados = cadastroContato.recuperarDados();

        if (dados.Nome == "") {
            $(".error.nome").html("Informe um nome");
            $(".error.nome").show("fast");
            retorno = false;
        }

        if (dados.Telefone == "") {
            $(".error.telefone").html("Informe o telefone");
            $(".error.telefone").show("fast");
            retorno = false;
        }

        if (dados.Email == "") {
            $(".error.email").html("Informe um email");
            $(".error.email").show("fast");
            retorno = false;
        } else {
            if (!cadastroContato.IsEmail(dados.Email)) {
                $(".error.email").html("Email inválido");
                $(".error.email").show("fast");
                retorno = false;
            }
        }

        if (dados.Celular == "") {
            $(".error.celular").html("Informe um número de celular");
            $(".error.celular").show("fast");
            retorno = false;
        }

        return retorno;
    }

    , salvar: function () {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Contato/SalvarCadastro",
            data: { configuracao: JSON.stringify(cadastroContato.recuperarDados()), r: (new Date().getTime()) },
            async: false,
            success: function (data) {
                if (data) {
                    jAlert('Salvo com sucesso!', 'Atenção', function () {
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

    , buscarContatos: function (nome, idEmpresa, page) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Contato/ListaContatos",
            data: { nome: nome, idEmpresa : idEmpresa, page: page, r: (new Date().getTime()) },
            async: false,
            success: function (data) {
                $(".table.contatos tbody tr").empty();
                $(".pagination").empty();
                
                if (data.Pesquisa.length > 0) {
                    var render = $('#contato-pesquisa-template').render(data.Pesquisa);
                    $(".table.contatos").find("tbody").append(render);

                    // ---- Paginação ------------------
                    $(".pagination").append("<li><a href='#'>«</a></li>");
                    for (var i = 1; i <= data.TotalPagina; i++) {
                        var renderPaginacao = $('#paginacao-template').render({ pagina: i, className: (page == i ? 'active' : '') });
                        $(".pagination").append(renderPaginacao);
                    }
                    $(".pagination").append("<li><a href='#'>»</a></li>");
                    
                } else {
                    var render = $('#contato-pesquisa-sem-registro-template').render({ Nome: "Sem registro!" });
                    $(".table.contatos").find("tbody").append(render);
                }
            }
        });
    }

    , IsEmail: function (email) {
        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test(email)) {
            return false;
        } else {
            return true;
        }
    }

};
