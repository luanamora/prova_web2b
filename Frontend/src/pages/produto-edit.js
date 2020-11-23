import React from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import SaveIcon from '@material-ui/icons/Save';
import AddBoxIcon from '@material-ui/icons/AddBox';
import { makeStyles } from '@material-ui/core/styles';
import Api from '../Api';
import { useEffect } from 'react';
import { withRouter, useParams } from 'react-router-dom';

var edit = false;

function ProdutoEdit() {
    const [cdProduto, setCdProduto] = React.useState("");
    const [dsProduto, setDsProduto] = React.useState("");
    const [cdMarca, setCdMarca] = React.useState("");
    const [dsObs, setDsObs] = React.useState("");
    const [nrValor, setNrValor] = React.useState("");
    const [dsUrl, setDsUrl] = React.useState("");
    let params = useParams();

    let imagens = [];

    const useStyles = makeStyles((theme) => ({
        button: {
            margin: theme.spacing(1),
        },
    }));
    const classes = useStyles();

    async function Save(event) {
        const produto = {
            "cdProduto": cdProduto,
            "dsProduto": dsProduto,
            "cdMarca": cdMarca,
            "dsObs": dsObs,
            "nrValor": nrValor
        };

        const respPost = await Api.put(`produto/${cdProduto}`, produto);
        window.location.href = "http://localhost:3000/produto-list";
    }

    async function SaveImage(event) {
        const nextImagem = await Api.get('imagemproduto/NextId');
        var idImagem = nextImagem.data;
        const imagem = {
            "cdImagem": idImagem,
            "dsLink": dsUrl,
            "cdProduto": cdProduto
        };
        const respPostImagem = await Api.post('imagemproduto', imagem);
        setDsUrl("");
    }

    useEffect(() => {
        const id = params.id;
        const GetData = async () => {
            const response = await Api.get(`produto/${id}`);
            console.log(response.data);
            setCdProduto(response.data.cdProduto);
            setDsProduto(response.data.dsProduto);
            setCdMarca(response.data.cdMarca);
            setDsObs(response.data.dsObs);
            setNrValor(response.data.nrValor);
        };
        GetData();

        const GetDataImage = async () => {
            const response = await Api.get(`imagemproduto/${id}`);
            console.log(response.data);
            imagens = response.data;
        };
        GetDataImage();
    }, []);

    return (
        <div>
            <h1> Edição de Produto </h1>
            <div>
                <div>
                    <TextField id="cdProduto" label="Código" value={cdProduto} onChange={e => setCdProduto(e.target.value)} required fullWidth />
                </div>
                <div>
                    <TextField id="dsProduto" label="Descrição" value={dsProduto} onChange={e => setDsProduto(e.target.value)} required fullWidth />
                </div>
                <div>
                    <TextField id="cdMarca" label="Marca" value={cdMarca} onChange={e => setCdMarca(e.target.value)} required fullWidth />
                </div>
                <div>
                    <TextField id="dsObs" label="Observação" value={dsObs} onChange={e => setDsObs(e.target.value)} required fullWidth />
                </div>
                <div>
                    <TextField id="nrValor" label="Valor" value={nrValor} onChange={e => setNrValor(e.target.value)} required fullWidth />
                </div>
                <div>
                    <TextField id="dsUrl" label="Url" value={dsUrl} onChange={e => setDsUrl(e.target.value)} required />
                    <Button
                        color="secondary"
                        size="large"
                        className={classes.button}
                        startIcon={<AddBoxIcon />}
                        onClick={SaveImage}>
                    </Button>
                </div>
            </div>
            <div>
                <Button
                    variant="contained"
                    color="secondary"
                    size="large"
                    className={classes.button}
                    startIcon={<SaveIcon />}
                    onClick={Save}>
                    Salvar
                </Button>
            </div>
            <div>
                {imagens.map((imagem, index) => (
                    <p key={index}>Link Imagem: {imagem.dsLink}</p>
                ))}
            </div>
        </div>
    );
}

export default withRouter(ProdutoEdit);