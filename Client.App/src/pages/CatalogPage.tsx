import { useEffect, useState } from "react";
import { Container } from "@mui/material";
import { getCatalog, getCategories } from "../http/catalogAPI";
import { CatalogResponse, Category } from "../types/types";

export default function CatalogPage() {
    const [catalog, setCatalog] = useState<CatalogResponse | null>(null);
    const [categories, setCategories] = useState<Category[]>([]);
    const [open, setOpen] = useState<boolean>(false);

    useEffect(() => {
        if (!catalog) {
            getCatalog({}).then(data => setCatalog(data));
        }
        if (categories.length === 0) {
            getCategories().then(data => setCategories(data));
        }
    }, [catalog, categories]);

    return (
        <Container>
        </Container>
    );
};
