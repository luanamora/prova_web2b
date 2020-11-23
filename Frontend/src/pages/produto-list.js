import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import { useState, useEffect } from 'react';
import Api from '../Api';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import { withRouter } from 'react-router-dom';

const columns = [
    { id: 'acoes', label: 'Ações', minWidth: 100, align: 'center' },
    { id: 'cdProduto', label: 'Código', minWidth: 200, align: 'left' },
    { id: 'dsProduto', label: 'Descrição', minWidth: 200, align: 'left' },
    { id: 'cdMarca', label: 'Marca', minWidth: 100, align: 'center' },
    { id: 'dsObs', label: 'Observação', minWidth: 200, align: 'center' },
    { id: 'nrValor', label: 'Valor', minWidth: 200, align: 'center' },
];

const useStyles = makeStyles({
    root: {
        width: '100%',
    },
    container: {
        maxHeight: 440,
    },
});

async function Delete (event, id) {
    await Api.delete('produto/' + id);
    console.log('Deletou');
    window.location.href = "http://localhost:3000/produto-list";
}

async function Edit (event, id) {
    window.location.href = `http://localhost:3000/produto-edit/${id}`;
}

function ProdutoList() {
    const classes = useStyles();
    const [page, setPage] = React.useState(0);
    const [data, setData] = useState([]);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);

    useEffect(() => {
        const GetData = async () => {
          const response = await Api.get('produto');
            setData(response.data);
        }
        GetData();
        console.log(data);
    }, []);

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(+event.target.value);
        setPage(0);
    };

    return (
        <Paper className={classes.root}>
            <TableContainer className={classes.container}>
                <Table stickyHeader aria-label="sticky table">
                    <TableHead>
                        <TableRow>
                            {columns.map((column) => (
                                <TableCell
                                    key={column.id}
                                    align={column.align}
                                    style={{ minWidth: column.minWidth }, { fontWeight: "bold" }}
                                >
                                    {column.label}
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row) => {
                            return (
                                <TableRow >
                                    <TableCell align="center">{row.acoes}
                                      <DeleteIcon onClick={(e) => Delete(e, row.cdProduto)}/>
                                      <EditIcon onClick={(e) => Edit(e, row.cdProduto)}/>
                                    </TableCell>
                                    <TableCell align="left">{row.cdProduto}</TableCell>
                                    <TableCell align="left">{row.dsProduto}</TableCell>
                                    <TableCell align="center">{row.cdMarca}</TableCell>
                                    <TableCell align="center">{row.dsObs}</TableCell>
                                    <TableCell align="center">{row.nrValor}</TableCell>
                                </TableRow>
                            );
                        })}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
                rowsPerPageOptions={[5, 10, 15]}
                component="div"
                count={data.length}
                rowsPerPage={rowsPerPage}
                page={page}
                onChangePage={handleChangePage}
                onChangeRowsPerPage={handleChangeRowsPerPage}
            />
        </Paper>
    );
}

export default withRouter(ProdutoList);