
/*
document.addEventListener('DOMContentLoaded', (event) => {
    const themeToggleButton = document.getElementById('bd-theme');
    const themeIcon = document.querySelector('.theme-icon-active');
    const themeText = document.getElementById('bd-theme-text');

    const setTheme = (theme) => {
        if (theme === 'dark') {
            document.body.classList.add('dark-mode');
            themeIcon.innerHTML = '<use href="#sun"></use>';
            themeText.textContent = 'Toggle theme (light)';
        } else if (theme === 'light') {
            document.body.classList.remove('dark-mode');
            themeIcon.innerHTML = '<use href="#moon"></use>';
            themeText.textContent = 'Toggle theme (dark)';
        } else {
            // Auto theme
            if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
                document.body.classList.add('dark-mode');
                themeIcon.innerHTML = '<use href="#sun"></use>';
                themeText.textContent = 'Toggle theme (light)';
            } else {
                document.body.classList.remove('dark-mode');
                themeIcon.innerHTML = '<use href="#moon"></use>';
                themeText.textContent = 'Toggle theme (dark)';
            }
        }
    };

    const getCurrentTheme = () => {
        if (document.body.classList.contains('dark-mode')) {
            return 'dark';
        } else if (!document.body.classList.contains('dark-mode')) {
            return 'light';
        } else {
            return 'auto';
        }
    };

    themeToggleButton.addEventListener('click', () => {
        const currentTheme = getCurrentTheme();
        let newTheme;

        if (currentTheme === 'dark') {
            newTheme = 'light';
        } else if (currentTheme === 'light') {
            newTheme = 'auto';
        } else {
            newTheme = 'dark';
        }

        setTheme(newTheme);
    });

    // Set initial theme based on preference
    setTheme('auto');
});
 */

    const editButton = document.getElementById('editButton');
    const saveButton = document.getElementById('saveButton');
    const cancelButton = document.getElementById('cancelButton');

    // Alterna entre os modos de visualização e edição
    editButton.addEventListener('click', () => toggleEditMode(true));
    cancelButton.addEventListener('click', () => toggleEditMode(false));

    function toggleEditMode(isEditing) {
        const inputs = document.querySelectorAll('#profileForm input, #profileForm select');
    const displays = document.querySelectorAll('#profileForm p');

        inputs.forEach(input => input.classList.toggle('d-none', !isEditing));
        displays.forEach(display => display.classList.toggle('d-none', isEditing));

    editButton.classList.toggle('d-none', isEditing);
    saveButton.classList.toggle('d-none', !isEditing);
    cancelButton.classList.toggle('d-none', !isEditing);
    }

