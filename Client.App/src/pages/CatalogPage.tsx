import { useEffect, useState } from "react";
import { Container } from "@mui/material";
import { getCatalog, getCategories } from "../http/catalogAPI";
import { CatalogResponse, Category } from "../types/types";

export default function CatalogPage() {
    const [catalog, setCatalog] = useState<CatalogResponse | null>(null);
    const [categories, setCategories] = useState<Category[]>([]);
    const [open, setOpen] = useState<boolean>(false);

    useEffect(() => {
        getCatalog({}).then(data => setCatalog(data));
        getCategories().then(data => setCategories(data));
    }, []);

    return (
        <Container>
        </Container>
    );
};
