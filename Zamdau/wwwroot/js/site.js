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

